using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using System.Numerics;
using static System.Console;

namespace GameCollection{
    class DodgeMeteor{
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 450;
        struct player{
            public Rectangle rect;
            public Vector2 speed;
            public Color color;

        }
        static player Player;
        struct meteors{
            public Rectangle rect;
            public Vector2 speed;
            public Color color;
        }
        struct coins{
            public Rectangle rect;
            public Vector2 speed;
            public Color color;
        }
        const int num_meteors = 15;
        const int num_coins = 3;
        private int Death = 0;
        private int CoinsFound = 0;
        static meteors[] Meteors = new meteors[num_meteors];
        static coins[] Coins = new coins[num_coins];

        public void InitGame(){
            Player.rect.x = ScreenWidth / 2.0f;
            Player.rect.y = ScreenHeight - 50;
            Player.rect.width = 20;
            Player.rect.height = 20;
            Player.speed.X = 10;
            Player.speed.Y  =10;
            Player.color= WHITE;


            for(int i = 0; i<num_meteors;i++){
                Meteors[i].rect.width = 20;
                Meteors[i].rect.height = 20;
                Meteors[i].rect.y = GetRandomValue(0, ScreenWidth);
                Meteors[i].rect.x = GetRandomValue(-ScreenHeight,-20);
                Meteors[i].speed.Y = 10;
                Meteors[i].speed.X = 10;
                Meteors[i].color = PURPLE;
            }
            for(int i = 0; i<num_coins;i++){
                Coins[i].rect.width = 20;
                Coins[i].rect.height = 20;
                Coins[i].rect.y = GetRandomValue(0, ScreenWidth);
                Coins[i].rect.x = GetRandomValue(-ScreenHeight,-20);
                Coins[i].speed.Y = 10;
                Coins[i].speed.X = 10;
                Coins[i].color = YELLOW;
            }
        }
        public void UpdateGame(){
            if(IsKeyDown(KEY_A)){
                Player.rect.x -= Player.speed.X;
            }
            if(IsKeyDown(KEY_D)){
                Player.rect.x += Player.speed.X;
            }
            if(Player.rect.x <= 0){
                Player.rect.x = 0;
            }
            if(Player.rect.x > ScreenWidth){
                Player.rect.x = ScreenWidth -1;
            }
            for (int i = 0; i < num_meteors; i++){
                Meteors[i].rect.y += Meteors[i].speed.Y;

            
                if (Meteors[i].rect.y > ScreenHeight)
            {
                Meteors[i].rect.x = GetRandomValue(0, ScreenWidth);
                Meteors[i].rect.y = GetRandomValue(-ScreenHeight, -20);
            }
                if(CheckCollisionRecs(Meteors[i].rect,Player.rect)){
                    Death++;
                }
        }
            for (int i = 0; i < num_coins; i++){
                Coins[i].rect.y += Coins[i].speed.Y;

                if (Coins[i].rect.y > ScreenHeight)
            {
                Coins[i].rect.x = GetRandomValue(0, ScreenWidth);
                Coins[i].rect.y = GetRandomValue(-ScreenHeight, -20);
            }
                if(CheckCollisionRecs(Coins[i].rect,Player.rect)){
                    CoinsFound++;
            }
        }
    }
        public void DrawGame(){
            BeginDrawing();
            ClearBackground(BLACK);
            DrawRectangleRec(Player.rect, Player.color);
            for (int i = 0; i < num_meteors; i++)
            {        
                DrawRectangleRec(Meteors[i].rect, Meteors[i].color);
            }
            DrawText(string.Format("Deaths : {0:000}", Death), 550, 12, 20, RED);
            DrawText(string.Format("Coins: {0:000}", CoinsFound), 400, 12, 20, GREEN);
  	        
            for (int i = 0; i < num_coins; i++)
            {        
                DrawRectangleRec(Coins[i].rect, Coins[i].color);
            }
  	        EndDrawing();

            EndDrawing();
        }
        public void Run(){
            InitWindow(ScreenWidth,ScreenHeight,"Dodge the meteors");
            InitGame();
            SetTargetFPS(60);
            while(!WindowShouldClose() && Death<=5){
                UpdateGame();
                DrawGame();
            }
            CloseWindow();
            WriteLine("Game ended.");
            WriteLine("Number of coins :{0}", CoinsFound);
        }
    }
}