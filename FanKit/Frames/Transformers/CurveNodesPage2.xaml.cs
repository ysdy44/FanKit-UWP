using FanKit.Transformers;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Numerics;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.NodeCollection">.
    /// </summary>
    public sealed partial class CurveNodesPage2 : Page
    {
        //CurveNodes
        NodeCollection NodeCollection = new NodeCollection();


        public NodeCollectionMode Mode;

        private SelfControlPointMode selfMode;
        public SelfControlPointMode SelfMode
        {
            get => this.selfMode;
            set
            {
                switch (value)
                {
                    case SelfControlPointMode.None:
                        {
                            this.AngleCheckBox.IsChecked = false;
                            this.LengthCheckBox.IsChecked = false;
                        }
                        break;
                    case SelfControlPointMode.Length:
                        {
                            this.AngleCheckBox.IsChecked = false;
                            this.LengthCheckBox.IsChecked = true;
                        }
                        break;
                    case SelfControlPointMode.Angle:
                        {
                            this.AngleCheckBox.IsChecked = true;
                            this.LengthCheckBox.IsChecked = false;
                        }
                        break;
                    case SelfControlPointMode.Disable:
                        {
                            this.AngleCheckBox.IsChecked = true;
                            this.LengthCheckBox.IsChecked = true;
                        }
                        break;
                }
                this.selfMode = value;
            }
        }

        public EachControlPointLengthMode EachLengthMode;
        public EachControlPointAngleMode EachAngleMode;


        TransformerRect _transformerRect;
        Vector2 _startingPoint;
        Node _oldNode;
        bool _isAdd = true;


        //@Construct
        public CurveNodesPage2()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CurveNodesPage2.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/CurveNodesPage2.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/CurveNodesPage2.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Transformers/CurveNodesPage2.xaml.cs"));

                this.MirroredRadioButton.IsChecked = true;
                this.EachLengthMode = EachControlPointLengthMode.Equal;
                this.EachAngleMode = EachControlPointAngleMode.Asymmetric;
            };


            #region Button


            //PenMode & NodeMode         
            this.PenModeButton.Tapped += (s, e) =>
            {
                this.NodeModeButton.IsChecked = false;
                this._isAdd = true;
            };
            this.NodeModeButton.Tapped += (s, e) =>
            {
                this.PenModeButton.IsChecked = false;
                this._isAdd = false;
            };

            //Button
            this.RemoveButton.Tapped += (s, e) =>
            {
                NodeCollection.RemoveCheckedNodes(this.NodeCollection);
                this.CanvasControl.Invalidate();
            };
            this.AddButton.Tapped += (s, e) =>
            {
                NodeCollection.Interpolation(this.NodeCollection);
                this.CanvasControl.Invalidate();
            };
            this.SharpButton.Tapped += (s, e) =>
            {
                NodeCollection.SharpCheckedNodes(this.NodeCollection);
                this.CanvasControl.Invalidate();
            };
            this.SmoothButton.Tapped += (s, e) =>
            {
                NodeCollection.SmoothCheckedNodes(this.NodeCollection);
                this.CanvasControl.Invalidate();
            };


            #endregion


            #region Mode


            //SelfMode
            this.AngleCheckBox.Tapped += (s, e) =>
            {
                switch (this.SelfMode)
                {
                    case SelfControlPointMode.None: this.SelfMode = SelfControlPointMode.Angle; break;
                    case SelfControlPointMode.Length: this.SelfMode = SelfControlPointMode.Angle; break;
                    case SelfControlPointMode.Angle: this.SelfMode = SelfControlPointMode.None; break;
                    case SelfControlPointMode.Disable: this.SelfMode = SelfControlPointMode.None; break;
                }
            };
            this.LengthCheckBox.Tapped += (s, e) =>
            {
                switch (this.SelfMode)
                {
                    case SelfControlPointMode.None: this.SelfMode = SelfControlPointMode.Length; break;
                    case SelfControlPointMode.Length: this.SelfMode = SelfControlPointMode.None; break;
                    case SelfControlPointMode.Angle: this.SelfMode = SelfControlPointMode.Length; break;
                    case SelfControlPointMode.Disable: this.SelfMode = SelfControlPointMode.None; break;
                }
            };

            //EachMode
            this.MirroredRadioButton.Tapped += (s, e) =>
            {
                this.EachLengthMode = EachControlPointLengthMode.Equal;
                this.EachAngleMode = EachControlPointAngleMode.Asymmetric;
            };
            this.DisconnectedRadioButton.Tapped += (s, e) =>
            {
                this.EachLengthMode = EachControlPointLengthMode.None;
                this.EachAngleMode = EachControlPointAngleMode.None;
            };
            this.AsymmetricRadioButton.Tapped += (s, e) =>
            {
                this.EachLengthMode = EachControlPointLengthMode.None;
                this.EachAngleMode = EachControlPointAngleMode.Asymmetric;
            };



            #endregion


            #region Draw


            this.CanvasControl.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
                this.CanvasTransformer.Size = e.NewSize;
            };
            this.CanvasControl.CreateResources += (s, args) =>
            {
                Vector2 center = new Vector2(this.CanvasTransformer.Width, this.CanvasTransformer.Height) / 2;
                this.NodeCollection = new NodeCollection
                {
                    new Node
                    {
                        Point = new Vector2(-144.6047f, -138.5997f) + center,
                        LeftControlPoint = new Vector2(-144.6047f, -138.5997f) + center,
                        RightControlPoint = new Vector2(-144.6047f, -138.5997f) + center,
                        IsChecked = true,
                        IsSmooth = true,
                    },
                    new Node
                    {
                        Point = new Vector2(13.37953f, -95.3983f)+ center,
                        LeftControlPoint = new Vector2(35.38268f, -57.79712f)+ center,
                        RightControlPoint = new Vector2(-8.623611f, -132.9995f) + center,
                        IsChecked = true,
                        IsSmooth = true,
                    },
                    new Node
                    {
                        Point = new Vector2(-12.58583f, 87.00745f)+ center,
                        LeftControlPoint = new Vector2(9.285034f, 126.0071f)+ center,
                        RightControlPoint = new Vector2(-34.4567f, 48.00778f)+ center,
                        IsChecked = true,
                        IsSmooth = true,
                    },
                    new Node
                    {
                        Point = new Vector2(144.6047f, 138.5997f) + center,
                        LeftControlPoint = new Vector2(144.6047f, 138.5997f)+ center,
                        RightControlPoint = new Vector2(144.6047f, 138.5997f) + center,
                        IsChecked = true,
                        IsSmooth = true,
                    },
                };
            };
            this.CanvasControl.Draw += (sender, args) =>
            {
                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();

                //DrawCrad
                var previousImage = new ColorSourceEffect { Color = Windows.UI.Colors.White };
                args.DrawingSession.DrawCrad(previousImage, this.CanvasTransformer);

                //DrawBound
                CanvasGeometry geometry = this.NodeCollection.CreateGeometry(sender);
                args.DrawingSession.DrawGeometry(geometry.Transform(matrix), Windows.UI.Colors.DodgerBlue);

                //DrawNodeCollection
                args.DrawingSession.DrawNodeCollection(this.NodeCollection, matrix);

                switch (this.Mode)
                {
                    case NodeCollectionMode.RectChoose:
                        {
                            CanvasGeometry canvasGeometry = this._transformerRect.ToRectangle(this.CanvasControl);
                            CanvasGeometry canvasGeometryTransform = canvasGeometry.Transform(matrix); 
                            args.DrawingSession.DrawGeometryDodgerBlue(canvasGeometryTransform);
                        }
                        break;
                }
            };


            #endregion


            #region CanvasOperator


            //Single
            this.CanvasOperator.Single_Start += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);

                this._startingPoint = point;

                if (this._isAdd)
                {
                    this.Mode = NodeCollectionMode.Add;
                    this.NodeCollection.Add(new Node { Point = canvasPoint, LeftControlPoint = canvasPoint, RightControlPoint = canvasPoint });
                    return;
                }

                Matrix3x2 matrix = this.CanvasTransformer.GetMatrix();
                this.Mode = NodeCollection.ContainsNodeCollectionMode(point, this.NodeCollection, matrix);

                switch (this.Mode)
                {
                    case NodeCollectionMode.Move:
                        this.NodeCollection.CacheTransform(isOnlySelected: true);
                        break;
                    case NodeCollectionMode.MoveSingleNodePoint:
                        this.NodeCollection.SelectionOnlyOne(this.NodeCollection.Index);
                        this._oldNode = this.NodeCollection[this.NodeCollection.Index];
                        break;
                    case NodeCollectionMode.MoveSingleNodeLeftControlPoint:
                        this._oldNode = this.NodeCollection[this.NodeCollection.Index];
                        break;
                    case NodeCollectionMode.MoveSingleNodeRightControlPoint:
                        this._oldNode = this.NodeCollection[this.NodeCollection.Index];
                        break;
                    case NodeCollectionMode.RectChoose:
                        this._transformerRect = new TransformerRect(canvasPoint, canvasPoint);
                        break;
                }
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Single_Delta += (point) =>
            {
                Matrix3x2 inverseMatrix = this.CanvasTransformer.GetInverseMatrix();
                Vector2 canvasStartingPoint = Vector2.Transform(this._startingPoint, inverseMatrix);
                Vector2 canvasPoint = Vector2.Transform(point, inverseMatrix);

                switch (this.Mode)
                {
                    case NodeCollectionMode.Add:
                        this.NodeCollection[this.NodeCollection.Count - 1] = new Node { Point = canvasPoint, LeftControlPoint = canvasPoint, RightControlPoint = canvasPoint };
                        break;
                    case NodeCollectionMode.Move:
                        {
                            Vector2 vector = canvasPoint - canvasStartingPoint;
                            this.NodeCollection.TransformAdd(vector, isOnlySelected: true);
                        }
                        break;
                    case NodeCollectionMode.MoveSingleNodePoint:
                        this.NodeCollection[this.NodeCollection.Index] = this._oldNode.Move(canvasPoint);
                        break;
                    case NodeCollectionMode.MoveSingleNodeLeftControlPoint:
                        this.NodeCollection[this.NodeCollection.Index] = Node.Controller(this.SelfMode, this.EachLengthMode, this.EachAngleMode, canvasPoint, _oldNode, isLeftControlPoint: true);
                        break;
                    case NodeCollectionMode.MoveSingleNodeRightControlPoint:
                        this.NodeCollection[this.NodeCollection.Index] = Node.Controller(this.SelfMode, this.EachLengthMode, this.EachAngleMode, canvasPoint, _oldNode, isLeftControlPoint: false);
                        break;
                    case NodeCollectionMode.RectChoose:
                        {
                            TransformerRect transformerRect = new TransformerRect(canvasStartingPoint, canvasPoint);
                            this._transformerRect = transformerRect;
                            this.NodeCollection.RectChoose(transformerRect);
                        }
                        break;
                }

                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Single_Complete += (point) =>
            {
                this.Mode = NodeCollectionMode.None;
                this.CanvasControl.Invalidate();
            };


            //Right
            this.CanvasOperator.Right_Start += (point) =>
            {
                this.CanvasTransformer.CacheMove(point);
            };
            this.CanvasOperator.Right_Delta += (point) =>
            {
                this.CanvasTransformer.Move(point);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Right_Complete += (point) =>
            {
                this.CanvasTransformer.Move(point);
                this.CanvasControl.Invalidate();
            };


            //Double
            this.CanvasOperator.Double_Start += (center, space) =>
            {
                this.CanvasTransformer.CachePinch(center, space);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Double_Delta += (center, space) =>
            {
                this.CanvasTransformer.Pinch(center, space);
                this.CanvasControl.Invalidate();
            };
            this.CanvasOperator.Double_Complete += (center, space) =>
            {
                this.CanvasControl.Invalidate();
            };

            //Wheel
            this.CanvasOperator.Wheel_Changed += (point, space) =>
            {
                if (space > 0)
                    this.CanvasTransformer.ZoomIn(point);
                else
                    this.CanvasTransformer.ZoomOut(point);

                this.CanvasControl.Invalidate();
            };


            #endregion

        }
    }
}
