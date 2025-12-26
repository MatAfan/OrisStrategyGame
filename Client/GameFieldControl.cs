using Common;
using Common.DTO;

namespace Client
{
    public sealed class GameFieldControl : Control
    {
        private List<BuildingStateDto> buildings = [];
        private int selectedPlace = -1;
        private const int CellSize = 60;
        private const int GridSize = 6;

        public event EventHandler<int>? PlaceClicked;

        public GameFieldControl()
        {
            Size = new Size(GridSize * CellSize + 10, GridSize * CellSize + 10);
            DoubleBuffered = true;
            MouseClick += OnMouseClick;
        }

        public void UpdateBuildings(List<BuildingStateDto> newBuildings)
        {
            buildings = newBuildings;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    int placeId = row * GridSize + col;
                    int x = col * CellSize + 5;
                    int y = row * CellSize + 5;

                    BuildingStateDto? building = buildings.FirstOrDefault(b => b.PlaceId == placeId);

                    Brush brush = Brushes.LightGray;
                    if (building != null)
                    {
                        switch (building.Type)
                        {
                            case BuildingType.Barracks:
                                brush = Brushes.IndianRed;
                                break;
                            case BuildingType.Barricade:
                            case BuildingType.DefenseTower:
                                brush = Brushes.SteelBlue;
                                break;
                            case BuildingType.Laboratory:
                            case BuildingType.AlchemyFurnace:
                                brush = Brushes.Gold;
                                break;
                            default:
                            {
                                brush = GameLogic.IsProducer(building.Type) ? Brushes.ForestGreen : Brushes.Orange;
                                break;
                            }
                        }
                    }

                    if (placeId == selectedPlace)
                    {
                        g.FillRectangle(Brushes.Yellow, x, y, CellSize - 2, CellSize - 2);
                        g.FillRectangle(brush, x + 3, y + 3, CellSize - 8, CellSize - 8);
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y, CellSize - 2, CellSize - 2);
                    }

                    g.DrawRectangle(Pens.Black, x, y, CellSize - 2, CellSize - 2);

                    if (building != null)
                    {
                        string text = building.Level.ToString();
                        var font = new Font("Arial", 14, FontStyle.Bold);
                        var size = g.MeasureString(text, font);
                        g.DrawString(text, font, Brushes.White,
                            x + (CellSize - size.Width) / 2,
                            y + (CellSize - size.Height) / 2);
                    }
                    else
                    {
                        string text = placeId.ToString();
                        var font = new Font("Arial", 8);
                        g.DrawString(text, font, Brushes.DarkGray, x + 2, y + 2);
                    }
                }
            }
        }

        private void OnMouseClick(object? sender, MouseEventArgs e)
        {
            int col = (e.X - 5) / CellSize;
            int row = (e.Y - 5) / CellSize;

            if (col is >= 0 and < GridSize && row is >= 0 and < GridSize)
            {
                int placeId = row * GridSize + col;
                selectedPlace = placeId;
                Invalidate();
                PlaceClicked?.Invoke(this, placeId);
            }
        }
    }

    public static class GameLogic
    {
        public static bool IsProducer(BuildingType type)
        {
            return type is BuildingType.Logging or BuildingType.Quarry or BuildingType.Mine or BuildingType.Farm;
        }
    }
}
