using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using System.Numerics;
using static System.Console;

namespace GameCollection{
    public class FlappyBird{
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 450;
        private bool Death =false;
        private int points = 0;

        struct player{
            public Rectangle rect;
            public Vector2 speed;
            public Color color;
        }
        static player Player;
        struct up_pipes{
            public Rectangle rect; 
            public Color color;
        }
        struct down_pipes{
            public Rectangle rect;
            public Color color;
        }
        
        const int num_pipes = 10;
        int[] choices = {0, ScreenHeight};
        static up_pipes[] UpPipes = new up_pipes[num_pipes];
        static down_pipes[] DownPipes = new down_pipes[num_pipes];
        public void InitGame(){
            Player.rect.x = 10;
            Player.rect.y = 200;
            Player.rect.width = 10;
            Player.rect.height = 10;
            Player.speed.X =3;
            Player.speed.Y = 2;
            Player.color = BLACK;

            for(int i = 0; i<num_pipes;i++){
                UpPipes[i].rect.width = DownPipes[i].rect.width = 40;
                UpPipes[i].rect.height = DownPipes[i].rect.height = GetRandomValue(100,ScreenHeight/2-5);
                UpPipes[i].rect.y =0;
                DownPipes[i].rect.y= ScreenHeight-DownPipes[i].rect.height;
                UpPipes[i].rect.x =DownPipes[i].rect.x = GetRandomValue(50,ScreenWidth-10);
                UpPipes[i].color = RED;
                DownPipes[i].color = YELLOW;

            }
        }
        public void UpdateGame(){
            Player.rect.x += Player.speed.X;
            if(IsKeyDown(KEY_UP)){
                Player.rect.y -= Player.speed.Y;
            }
            else{
                Player.rect.y += Player.speed.Y;
            }
            if(Player.rect.x >= ScreenWidth){
                Player.rect.x = 10;
                for(int i =0;i<num_pipes;i++){
                    UpPipes[i].rect.y = 0;
                    DownPipes[i].rect.y = ScreenHeight-DownPipes[i].rect.height;
                    UpPipes[i].rect.x =DownPipes[i].rect.x = GetRandomValue(50,ScreenWidth);
                    WriteLine(DownPipes[i].rect.y);
                }
                points += 50;
            }
            if(Player.rect.y >= ScreenHeight){
                Player.rect.y -= 1;
            }
            for(int i = 0;i< num_pipes;i++){
                if(CheckCollisionRecs(UpPipes[i].rect,Player.rect)||CheckCollisionRecs(DownPipes[i].rect,Player.rect)){
                    Death = true;
                }
            }
        }
        public void DrawGame(){
            BeginDrawing();
            ClearBackground(WHITE);
            DrawRectangleRec(Player.rect, Player.color);
            for (int i = 0; i < num_pipes; i++)
            {        
                DrawRectangleRec(UpPipes[i].rect,UpPipes[i].color);
                DrawRectangleRec(DownPipes[i].rect,DownPipes[i].color);
            }
            DrawText(string.Format("POINTS : {0:000}", points), 550, 12, 20,GREEN);
  	        EndDrawing();

            EndDrawing();
        }
        public void Run(){
            InitWindow(ScreenWidth,ScreenHeight,"FlappyBird (modified)");
            InitGame();
            SetTargetFPS(60);
            while(!WindowShouldClose() && !Death){
                UpdateGame();
                DrawGame();
            }
            CloseWindow();
            WriteLine("Game ended.");
            WriteLine("Number of points :{0}", points);
        }
    }
}