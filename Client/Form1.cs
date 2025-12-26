using Common;
using Common.DTO;
using Common.Services;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    /// <summary>
    /// Главная форма клиентского приложения игры.
    /// </summary>
    public partial class Form1 : Form
    {
        private TcpClient client = null!;
        private NetworkStream stream = null!;
        private int myId;
        private int currentCycle;
        private int currentTurn;
        private bool myTurn;
        private Dictionary<string, int> resources = new();
        private List<BuildingStateDto> buildings = [];
        private int soldiers;
        private int defense;
        private int selectedPlace = -1;
        private bool waitingForResponse;
        private List<PlayerInfoDto> playersList = [];
        private int currentTurnPlayerId;

        public Form1()
        {
            InitializeComponent();
            LoadArchetypes();
        }

        /// <summary>
        /// Загружает список архетипов в выпадающий список.
        /// </summary>
        private void LoadArchetypes()
        {
            foreach (ArchetypeType archetype in Enum.GetValues<ArchetypeType>())
                cmbArchetype.Items.Add(archetype.GetName());
            cmbArchetype.SelectedIndex = (int)ArchetypeType.Neutral;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки подключения к серверу.
        /// </summary>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Извлекаем все числа из строки
                var numbers = System.Text.RegularExpressions.Regex.Matches(txtAddress.Text, @"\d+")
                    .Cast<System.Text.RegularExpressions.Match>()
                    .Select(m => m.Value)
                    .ToList();

                if (numbers.Count != 5)
                {
                    MessageBox.Show(@"Неверный формат. Введите 5 чисел: 4 для IP-адреса и 1 для порта.");
                    return;
                }

                // Проверяем октеты IP-адреса (0-255)
                int[] octets = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    if (!int.TryParse(numbers[i], out octets[i]) || octets[i] < 0 || octets[i] > 255)
                    {
                        MessageBox.Show($@"Неверное значение октета IP-адреса: {numbers[i]}. Допустимый диапазон: 0-255.");
                        return;
                    }
                }

                // Проверяем порт (1-65535)
                if (!int.TryParse(numbers[4], out int port) || port < 1 || port > 65535)
                {
                    MessageBox.Show($@"Неверный порт: {numbers[4]}. Допустимый диапазон: 1-65535.");
                    return;
                }

                string ipAddress = $"{octets[0]}.{octets[1]}.{octets[2]}.{octets[3]}";
                client = new TcpClient(ipAddress, port);
                stream = client.GetStream();

                var dto = new JoinDto
                {
                    Nickname = txtNickname.Text,
                    Email = txtEmail.Text
                };
                SendMsg(MessageType.JOIN, dto);

                Task.Run(ReceiveLoop);

                btnConnect.Enabled = false;
                Log($"Подключено к серверу {ipAddress}:{port}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Обрабатывает выбор архетипа игрока.
        /// </summary>
        private void BtnSelectArchetype_Click(object sender, EventArgs e)
        {
            ArchetypeType arch = (ArchetypeType)cmbArchetype.SelectedIndex;

            var dto = new ArchetypeDto { ArchetypeType = arch };
            SendMsg(MessageType.ARCHETYPE, dto);

            cmbArchetype.Visible = false;
            btnSelectArchetype.Visible = false;
            Log("Архетип выбран");
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки строительства здания.
        /// </summary>
        private void BtnBuild_Click(object sender, EventArgs e)
        {
            if (!myTurn)
            {
                MessageBox.Show(@"Не ваш ход");
                return;
            }

            if (waitingForResponse)
            {
                MessageBox.Show(@"Дождитесь ответа сервера");
                return;
            }

            if (selectedPlace < 0)
            {
                MessageBox.Show(@"Выберите место на поле");
                return;
            }

            var buildForm = new BuildForm();
            if (buildForm.ShowDialog() == DialogResult.OK)
            {
                var dto = new BuildRequestDto
                {
                    PlaceId = selectedPlace,
                    Type = buildForm.SelectedType
                };
                SendMsgWithWait(MessageType.BUILD, dto);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки улучшения здания.
        /// </summary>
        private void BtnUpgrade_Click(object sender, EventArgs e)
        {
            if (!myTurn)
            {
                MessageBox.Show(@"Не ваш ход");
                return;
            }

            if (waitingForResponse)
            {
                MessageBox.Show(@"Дождитесь ответа сервера");
                return;
            }

            if (lstBuildings.SelectedIndex < 0)
            {
                MessageBox.Show(@"Выберите здание");
                return;
            }

            var b = buildings[lstBuildings.SelectedIndex];
            var dto = new UpgradeRequestDto { PlaceId = b.PlaceId };
            SendMsgWithWait(MessageType.UPGRADE, dto);
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки создания солдат.
        /// </summary>
        private void BtnMakeSoldiers_Click(object sender, EventArgs e)
        {
            if (!myTurn)
            {
                MessageBox.Show(@"Не ваш ход");
                return;
            }

            if (waitingForResponse)
            {
                MessageBox.Show(@"Дождитесь ответа сервера");
                return;
            }

            bool hasBarracks = buildings.Any(b => b.Type == BuildingType.Barracks);

            if (!hasBarracks)
            {
                MessageBox.Show(@"Нет казарм");
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox("Сколько солдат?", "Создать солдат", "1");
            if (int.TryParse(input, out int count) && count > 0)
            {
                var dto = new MakeSoldiersRequestDto
                {
                    BarracksId = 0,
                    Count = count
                };
                SendMsgWithWait(MessageType.MAKE_SOLDIERS, dto);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки атаки на другого игрока.
        /// </summary>
        private void BtnAttack_Click(object sender, EventArgs e)
        {
            if (!myTurn)
            {
                MessageBox.Show(@"Не ваш ход");
                return;
            }

            if (waitingForResponse)
            {
                MessageBox.Show(@"Дождитесь ответа сервера");
                return;
            }

            string targetStr = Microsoft.VisualBasic.Interaction.InputBox("ID цели:", "Атака", myId == 2 ? "1" : "2");
            string countStr = Microsoft.VisualBasic.Interaction.InputBox("Сколько солдат?", "Атака", "1");

            if (int.TryParse(targetStr, out int targetId) && int.TryParse(countStr, out int count))
            {
                var dto = new AttackRequestDto
                {
                    ToPlayerId = targetId,
                    Soldiers = count
                };
                SendMsgWithWait(MessageType.ATTACK, dto);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки завершения хода.
        /// </summary>
        private void BtnEndTurn_Click(object sender, EventArgs e)
        {
            if (!myTurn)
            {
                MessageBox.Show(@"Не ваш ход");
                return;
            }

            SendMsg(MessageType.END_TURN, new { });
            myTurn = false;
            btnEndTurn.Enabled = false;
        }

        /// <summary>
        /// Обрабатывает выбор места на игровом поле.
        /// </summary>
        private void GameField_PlaceClicked(object sender, int placeId)
        {
            selectedPlace = placeId;
        }

        /// <summary>
        /// Отправляет сообщение на сервер.
        /// </summary>
        private void SendMsg(MessageType type, object payload)
        {
            try
            {
                var msg = MessageSerializer.Serialize(type, payload);
                string str = MessageParser.Serialize(msg);
                byte[] data = ByteConverter.StringToBytes(str);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Log("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Отправляет сообщение на сервер и активирует режим ожидания ответа.
        /// </summary>
        private void SendMsgWithWait(MessageType type, object payload)
        {
            waitingForResponse = true;
            lblWaiting.Text = @"Ожидание ответа...";
            lblWaiting.Visible = true;
            SendMsg(type, payload);
        }

        /// <summary>
        /// Сбрасывает состояние ожидания ответа от сервера.
        /// </summary>
        private void ClearWaiting()
        {
            waitingForResponse = false;
            lblWaiting.Visible = false;
        }

        /// <summary>
        /// Цикл приёма сообщений от сервера в фоновом потоке.
        /// </summary>
        private void ReceiveLoop()
        {
            byte[] buffer = new byte[8192];
            int offset = 0;

            try
            {
                while (true)
                {
                    int read = stream.Read(buffer, offset, buffer.Length - offset);
                    if (read == 0) break;
                    offset += read;

                    while (ByteConverter.TryReadMessage(buffer, 0, offset, out string msgStr, out int bytesUsed))
                    {
                        var msg = MessageParser.Parse(msgStr);
                        HandleMsg(msg);

                        Buffer.BlockCopy(buffer, bytesUsed, buffer, 0, offset - bytesUsed);
                        offset -= bytesUsed;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Ошибка приема: " + ex.Message);
            }
        }

        /// <summary>
        /// Обрабатывает входящее сообщение от сервера.
        /// </summary>
        private void HandleMsg(NetworkMessage msg)
        {
            Invoke((MethodInvoker)delegate
            {
                switch (msg.Type)
                {
                    case MessageType.RESPONSE:
                        var resp = MessageDeserializer.Deserialize<ResponseDto>(msg);
                        Log(resp.Message ?? "");
                        ClearWaiting();
                        break;

                    case MessageType.START_GAME:
                        var start = MessageDeserializer.Deserialize<StartGameDto>(msg);
                        myId = start.PlayerId;
                        Log($"⚔️ Битва началась! Вы боец #{myId}");

                        playersList = start.Players;
                        UpdatePlayersList();
                        Log("Бойцы на плантации:");
                        foreach (var pl in start.Players)
                            Log($"  #{pl.Id}: {pl.Nickname}");

                        cmbArchetype.Visible = true;
                        btnSelectArchetype.Visible = true;
                        break;

                    case MessageType.START_TURN:
                        var turn = MessageDeserializer.Deserialize<StartTurnDto>(msg);
                        currentCycle = turn.Cycle;
                        currentTurn = turn.Turn;
                        currentTurnPlayerId = turn.PlayerId;
                        myTurn = (turn.PlayerId == myId);

                        lblCycle.Text = $@"Цикл: {currentCycle}";
                        lblTurn.Text = $@"Ход: {currentTurn}";
                        UpdatePlayersList();

                        if (myTurn)
                        {
                            Log("Ваш ход!");
                            btnEndTurn.Enabled = true;
                            pnlGame.Visible = true;
                            AnimateTurnStart();
                        }
                        else
                        {
                            Log($"Ход игрока {turn.PlayerId}");
                        }
                        break;

                    case MessageType.STATE:
                        var state = MessageDeserializer.Deserialize<StateDto>(msg);
                        UpdateState(state);
                        break;

                    case MessageType.PRODUCTION_RESULT:
                        var prod = MessageDeserializer.Deserialize<ProductionResultDto>(msg);
                        var prodSb = new StringBuilder();
                        foreach (var r in prod.ProducedResources)
                        {
                            string name = ResourceNames.GetName(r.Key);
                            prodSb.Append($"{name}:{r.Value} ");
                        }
                        Log($"Произведено: {prodSb}");
                        break;

                    case MessageType.ATTACK_TARGET:
                        var atk = MessageDeserializer.Deserialize<AttackTargetDto>(msg);
                        var stolenSb = new StringBuilder();
                        foreach (var r in atk.StolenResources)
                        {
                            string name = ResourceNames.GetName(r.Key);
                            stolenSb.Append($"{name}:{r.Value} ");
                        }
                        Log($"Атака: отправлено {atk.Sent}, потеряно {atk.Lost}, украдено: {stolenSb}");
                        AnimateAttack();
                        break;

                    case MessageType.ATTACK_RECEIVED:
                        var atkRecv = MessageDeserializer.Deserialize<AttackReceivedDto>(msg);
                        var lostSb = new StringBuilder();
                        foreach (var r in atkRecv.LostResources)
                        {
                            string name = ResourceNames.GetName(r.Key);
                            lostSb.Append($"{name}:{r.Value} ");
                        }
                        Log($"!!! ВАС АТАКОВАЛ {atkRecv.FromNickname} !!!");
                        Log($"Солдат атаковало: {atkRecv.SoldiersAttacked}, уничтожено: {atkRecv.SoldiersLost}");
                        Log($"Потеряно ресурсов: {lostSb}");
                        AnimateAttack();
                        break;

                    case MessageType.TURN_ENDED:
                        var ended = MessageDeserializer.Deserialize<TurnEndedDto>(msg);
                        Log($"Ход завершен. Следующий: {ended.NextPlayerId}");
                        break;

                    case MessageType.GAME_END:
                        var end = MessageDeserializer.Deserialize<GameEndDto>(msg);
                        var winSb = new StringBuilder();
                        winSb.AppendLine("⚔️ БИТВА НА ПЛАНТАЦИИ ОКОНЧЕНА ⚔️");
                        winSb.AppendLine();
                        winSb.AppendLine("Результаты:");
                        foreach (var score in end.AllScores)
                        {
                            winSb.AppendLine($"{score.Nickname}: {score.Points} очков");
                        }
                        winSb.AppendLine();
                        winSb.AppendLine($"Победитель: игрок {end.WinnerPlayerId}");
                        winSb.AppendLine($"Очки победителя: {end.Points}");

                        MessageBox.Show(winSb.ToString(), "Конец игры");
                        break;

                    case MessageType.PLAYER_LEFT:
                        var left = MessageDeserializer.Deserialize<PlayerLeftDto>(msg);
                        Log($"{left.Nickname} покинул игру!");
                        playersList = left.RemainingPlayers;
                        UpdatePlayersList();
                        break;
                }
            });
        }

        /// <summary>
        /// Обновляет отображение состояния игрока на форме.
        /// </summary>
        private void UpdateState(StateDto state)
        {
            resources = state.Resources;
            soldiers = state.Soldiers;
            defense = state.Defense;
            buildings = state.Buildings;

            var resSb = new StringBuilder();
            resSb.AppendLine("Ресурсы:");
            foreach (var r in resources)
            {
                string name = ResourceNames.GetName(r.Key);
                resSb.AppendLine($"{name}: {r.Value}");
            }
            resSb.AppendLine();
            resSb.AppendLine($"Солдаты: {soldiers}");
            resSb.Append($"Защита: {defense}%");
            lblResources.Text = resSb.ToString();

            lstBuildings.Items.Clear();
            foreach (var b in buildings)
            {
                string name = BuildingNames.GetName(b.Type);
                lstBuildings.Items.Add($"Место {b.PlaceId}: {name} ур.{b.Level}");
            }

            gameField.UpdateBuildings(buildings);
        }

        /// <summary>
        /// Воспроизводит анимацию начала хода игрока.
        /// </summary>
        private void AnimateTurnStart()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            int step = 0;
            Color origColor = pnlGame.BackColor;

            timer.Tick += (s, e) =>
            {
                step++;
                switch (step)
                {
                    case <= 5:
                        pnlGame.BackColor = Color.LightGreen;
                        break;
                    case <= 10:
                        pnlGame.BackColor = origColor;
                        break;
                    default:
                        timer.Stop();
                        timer.Dispose();
                        break;
                }
            };
            timer.Start();
        }

        /// <summary>
        /// Воспроизводит анимацию атаки.
        /// </summary>
        private void AnimateAttack()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            int step = 0;
            Color origColor = pnlGame.BackColor;

            timer.Tick += (s, e) =>
            {
                step++;
                pnlGame.BackColor = step % 2 == 1 ? Color.Red : origColor;

                if (step >= 6)
                {
                    pnlGame.BackColor = origColor;
                    timer.Stop();
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        /// <summary>
        /// Записывает сообщение в лог на форме.
        /// </summary>
        private void Log(string msg)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke((MethodInvoker)delegate { Log(msg); });
            else
                txtLog.AppendText(msg + "\r\n");
        }

        /// <summary>
        /// Обновляет список игроков на форме.
        /// </summary>
        private void UpdatePlayersList()
        {
            lstPlayers.Items.Clear();
            foreach (var pl in playersList)
            {
                string status = "";
                if (pl.Id == myId)
                    status = " (ВЫ)";
                if (pl.Id == currentTurnPlayerId)
                    status += " ⚔️ ходит";

                lstPlayers.Items.Add($"#{pl.Id}: {pl.Nickname}{status}");
            }
        }
    }
}
