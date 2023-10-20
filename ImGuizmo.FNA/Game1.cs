using ImGuiNET;
using ImGuiNET.SampleProgram.XNA;

using ImGuizmoNET;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using SDL2;

namespace ImGuizmoFNA
{
    public class Game1 : Game
    {
        private ImGuiRenderer _imGuiRenderer;

        private Matrix testMat = Matrix.Identity;
        private Matrix testMatDelta = Matrix.Identity;

        private bool isSnap = false;
        private Vector3 posSnap = new Vector3(0.25f, 0.25f, 0.25f);
        private Vector3 rotSnap = new Vector3(10f, 10f, 10f);
        private Vector3 scaleSnap = new Vector3(0.1f, 0.1f, 0.1f);

        private Matrix view = Matrix.CreateTranslation(0f, 0f, -10f);

        private OPERATION op = OPERATION.TRANSLATE;
        private bool isLocal = false;

        private KeyboardState _prevKB;

        public Game1()
        {
            GraphicsDeviceManager gdm = new GraphicsDeviceManager(this);
            gdm.PreferredBackBufferWidth = 1024;
            gdm.PreferredBackBufferHeight = 768;
            gdm.SynchronizeWithVerticalRetrace = true;

            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            Window.Title = "ImGuizmo Sample";
        }

        protected override void Initialize()
        {
            _imGuiRenderer = new ImGuiRenderer(this);
            _imGuiRenderer.RebuildFontAtlas();

            SDL.SDL_MaximizeWindow(Window.Handle);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalSeconds > 0f)
            {
                base.Draw(gameTime);
                GraphicsDevice.Clear(Color.CornflowerBlue);

                _imGuiRenderer.BeforeLayout(gameTime);
                DrawUI();
                _imGuiRenderer.AfterLayout();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState curKb = Keyboard.GetState();

            if (curKb.IsKeyDown(Keys.W) && _prevKB.IsKeyUp(Keys.W))
            {
                op = OPERATION.TRANSLATE;
            }

            if (curKb.IsKeyDown(Keys.E) && _prevKB.IsKeyUp(Keys.E))
            {
                op = OPERATION.ROTATE;
            }

            if (curKb.IsKeyDown(Keys.R) && _prevKB.IsKeyUp(Keys.R))
            {
                op = OPERATION.SCALE;
            }

            isSnap = curKb.IsKeyDown(Keys.LeftControl);

            _prevKB = curKb;
        }

        protected void DrawUI()
        {
            if (ImGui.Begin("Test"))
            {
                ImGui.Checkbox("Local Space", ref isLocal);
                ImGui.End();
            }

            float aspect = (float)GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height;
            Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), aspect, 0.01f, 1000f);

            ImGuizmo.SetRect(0f, 0f, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            Matrix id = Matrix.Identity;
            ImGuizmo.DrawGrid(ref view.M11, ref proj.M11, ref id.M11, 100f);

            ImGuizmo.DrawCubes(ref view.M11, ref proj.M11, ref testMat.M11, 1);

            Vector3 snap = Vector3.Zero;

            if (isSnap)
            {
                switch (op)
                {
                    case OPERATION.TRANSLATE:
                        snap = posSnap;
                        break;
                    case OPERATION.ROTATE:
                        snap = rotSnap;
                        break;
                    case OPERATION.SCALE:
                        snap = scaleSnap;
                        break;
                }
            }

            ImGuizmo.Manipulate(ref view.M11, ref proj.M11, op, isLocal ? MODE.LOCAL : MODE.WORLD, ref testMat.M11, ref testMatDelta.M11, ref snap.X);

            ImGuizmo.ViewManipulate(ref view.M11, 10f, new System.Numerics.Vector2(0f, 0f), new System.Numerics.Vector2(128f, 128f), 0x55010101);
        }
    }
}
