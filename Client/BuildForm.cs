using Common;

namespace Client
{
    public class BuildForm : Form
    {
        private ComboBox cmbType;
        private Button btnOk;
        private Button btnCancel;

        public BuildingType SelectedType { get; private set; }

        public BuildForm()
        {
            InitializeComponent();
            LoadTypes();
        }

        private void InitializeComponent()
        {
            cmbType = new ComboBox();
            btnOk = new Button();
            btnCancel = new Button();

            cmbType.Location = new Point(20, 20);
            cmbType.Size = new Size(250, 25);
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;

            btnOk.Location = new Point(20, 60);
            btnOk.Size = new Size(100, 30);
            btnOk.Text = @"OK";
            btnOk.Click += BtnOk_Click;

            btnCancel.Location = new Point(170, 60);
            btnCancel.Size = new Size(100, 30);
            btnCancel.Text = @"Отмена";
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

            ClientSize = new Size(300, 110);
            Controls.Add(cmbType);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Text = @"Построить здание";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
        }

        private void LoadTypes()
        {
            foreach (BuildingType type in Enum.GetValues<BuildingType>())
                cmbType.Items.Add(new BuildingTypeItem(type, BuildingNames.GetName(type)));
            cmbType.SelectedIndex = 0;
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            if (cmbType.SelectedItem is BuildingTypeItem item)
                SelectedType = item.Type;
            DialogResult = DialogResult.OK;
        }

        private record BuildingTypeItem(BuildingType Type, string DisplayName)
        {
            public override string ToString() => DisplayName;
        }
    }
}
