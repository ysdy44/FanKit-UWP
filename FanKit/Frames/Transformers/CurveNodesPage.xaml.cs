using FanKit.Win2Ds;
using System.Numerics;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    public sealed partial class CurveNodesPage : Page
    {
        //CurveNodes
        CurveNodes CurveNode = new CurveNodes();

        bool IsMove;

        public CurveNodesPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CurveNodesPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CurveNodesPage.xaml.cs.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CurveNodes.cs.txt");
            };


            //PenMode & NodeMode         
            this.PenModeButton.Tapped += (sender, e) => { this.CurveNode.EditMode = NodeEditMode.Add; this.NodeModeButton.IsChecked = false; };
            this.NodeModeButton.Tapped += (sender, e) => { this.CurveNode.EditMode = NodeEditMode.EditMove; this.PenModeButton.IsChecked = false; };

            //Button
            this.Remove.Tapped += (sender, e) => { this.CurveNode.Remove(); this.CanvasControl.Invalidate(); };
            this.Add.Tapped += (sender, e) => { this.CurveNode.Interpolation(); this.CanvasControl.Invalidate(); };
            this.Sharp.Tapped += (sender, e) => { this.CurveNode.Sharp(); this.CanvasControl.Invalidate(); };
            this.Smooth.Tapped += (sender, e) => { this.CurveNode.Smooth(); this.CanvasControl.Invalidate(); };

            //Radio
            this.MirroredButton.Tapped += (sender, e) => this.CurveNode.ControlMode = NodeControlMode.Mirrored;
            this.DisconnectedButton.Tapped += (sender, e) => this.CurveNode.ControlMode = NodeControlMode.Disconnected;
            this.AsymmetricButton.Tapped += (sender, e) => this.CurveNode.ControlMode = NodeControlMode.Asymmetric;


            //Canvas
            this.CanvasControl.Draw += (sender, args) => this.CurveNode.Draw(sender, args.DrawingSession);
            this.CanvasControl.CreateResources+=(sender, args) =>
            {
                Vector2 center = new Vector2((float)(sender.ActualWidth / 2), (float)(sender.ActualHeight / 2));
                this.CurveNode.Nodes.Add(new Node(new Vector2(-144.6047f, -138.5997f) + center, new Vector2(-144.6047f, -138.5997f) + center, new Vector2(-144.6047f, -138.5997f) + center));
                this.CurveNode.Nodes.Add(new Node(new Vector2(13.37953f, -95.3983f) + center, new Vector2(35.38268f, -57.79712f) + center, new Vector2(-8.623611f, -132.9995f) + center, NodeChooseMode.Vector));
                this.CurveNode.Nodes.Add(new Node(new Vector2(-12.58583f, 87.00745f) + center, new Vector2(9.285034f, 126.0071f) + center, new Vector2(-34.4567f, 48.00778f) + center, NodeChooseMode.Vector));
                this.CurveNode.Nodes.Add(new Node(new Vector2(144.6047f, 138.5997f) + center, new Vector2(144.6047f, 138.5997f) + center, new Vector2(144.6047f, 138.5997f) + center));
            };


            //Pointer
            this.CanvasControl.PointerPressed += (sender, e) =>
            {
                this.IsMove = true;

                this.CurveNode.Operator_Start(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                this.CanvasControl.Invalidate();
            };
            this.CanvasControl.PointerMoved += (sender, e) =>
            {
                if (this.IsMove)
                {
                    this.CurveNode.Operator_Delta(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                    this.CanvasControl.Invalidate();
                }
            };
            this.CanvasControl.PointerReleased += (sender, e) =>
            {
                if (this.IsMove)
                {
                    this.IsMove = false;

                    this.CurveNode.Operator_Complete(e.GetCurrentPoint(this.CanvasControl).Position.ToVector2());
                    this.CanvasControl.Invalidate();
                }

                this.Remove.IsEnabled = this.Add.IsEnabled = this.Sharp.IsEnabled = this.Smooth.IsEnabled = this.CurveNode.IsAnyChoose;
            };
        }           
    }
}

