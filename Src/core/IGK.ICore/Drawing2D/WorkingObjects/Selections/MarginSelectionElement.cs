using IGK.ICore.Drawing2D.Dependency;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WorkingObjects.Selections
{
#if DEBUG
       [Core2DDrawingSelectionAttribute("MarginSelection",
        typeof(Mecanism),
        Keys =enuKeys.Shift | enuKeys.M)]
#endif
    /// <summary>
    /// used to configure maring on a drawing 2D surface
    /// </summary>
    public sealed class MarginSelectionElement : SelectionElement
    {
           /// <summary>
           /// .ctr
           /// </summary>
           public MarginSelectionElement(){
           }
           
           new class Mecanism : Core2DDrawingSurfaceMecanismBase<Core2DDrawingLayeredElement>, ICoreSnippetMecanismObserver
           {
               void ResetAll() {
                   var r = this.Element;
                   if (r == null)
                       return;
                   r.SetValue(Core2DMarginDependency.LeftProperty, null);
                   r.SetValue(Core2DMarginDependency.RightProperty, null);
                   r.SetValue(Core2DMarginDependency.TopProperty, null);
                   r.SetValue(Core2DMarginDependency.BottomProperty,null);
               }
               protected override void GenerateActions()
               {
                   base.GenerateActions();
               }
               protected internal override void GenerateSnippets()
               {
                   this.DisposeSnippet();
                   this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, 0, 0, RenderSnippet));
                   this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, 0, 1, RenderSnippet));
                   this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, 0, 2, RenderSnippet));
                   this.RegSnippets.Add(CurrentSurface.CreateSnippet(this, 0, 3, RenderSnippet));
               }
               protected internal override void InitSnippetsLocation()
               {
                   var t = this.Element;
                   if (t == null)
                       return;

                   var b = t.GetValue<CoreUnit>(Core2DMarginDependency.LeftProperty);
                   float v_left = b!=null? b.GetPixel() : 0;
                   b = t.GetValue<CoreUnit>(Core2DMarginDependency.TopProperty);
                   float v_top = b != null ? b.GetPixel() : 0;
                   b = t.GetValue<CoreUnit>(Core2DMarginDependency.RightProperty);
                   float v_right = b != null ? b.GetPixel() : 0;
                   b = t.GetValue<CoreUnit>(Core2DMarginDependency.BottomProperty);
                   float v_bottom = b != null ? b.GetPixel() : 0; 
                   var v_rc = t.GetBound();
                   var v_docBound = this.CurrentSurface.CurrentDocument.Bounds;

                   this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_left, v_rc.MiddleLeft.Y));
                   this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, v_top));
                   this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_docBound.Right - v_right, v_rc.Center.Y));
                   this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, v_docBound.Bottom - v_bottom));
                   
               }
               
               protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
               {
                   var t = this.Element;
                   if (t == null)
                       return;
                   CoreUnit u = 0;
                   CoreUnit m = 0;
                   var v_docBound = this.CurrentSurface.CurrentDocument.Bounds;
                   switch (Snippet.Index)
                   {
                       case 0:
                           u = t.GetValue<CoreUnit>(Core2DMarginDependency.LeftProperty);
                           m = e.FactorPoint.X .ToString();
                           t.SetValue(Core2DMarginDependency.LeftProperty, m.ToString(u.UnitType ));
                           break;
                       case 1:
                           u = t.GetValue<CoreUnit>(Core2DMarginDependency.TopProperty);
                           m = e.FactorPoint.Y.ToString();
                           t.SetValue(Core2DMarginDependency.TopProperty, m.ToString(u.UnitType));
                           break;
                       case 2:
                           u = t.GetValue<CoreUnit>(Core2DMarginDependency.RightProperty);
                           m = (v_docBound.Right - e.FactorPoint.X).ToString();
                           t.SetValue(Core2DMarginDependency.RightProperty, m.ToString(u.UnitType));
                           break;
                       case 3:
                              u = t.GetValue<CoreUnit>(Core2DMarginDependency.BottomProperty);
                              m = (v_docBound.Bottom - e.FactorPoint.Y).ToString();
                           t.SetValue(Core2DMarginDependency.BottomProperty, m.ToString(u.UnitType));
                           break;
                   }
                   this.Snippet.Location = e.Location;
                   this.Invalidate();
               }
               
               protected override void EndSnippetEdit(CoreMouseEventArgs e)
               {
                   base.EndSnippetEdit(e);
               }
            
               private void RenderSnippet(ICoreSnippet snippet , ICoreGraphics device, float inflate)
               {
                   var t = this.Element;
                   if (t == null)
                       return;
                   var p = device.Save();
                   device.ResetTransform();
                   Vector2f s = Vector2f.Zero;//start point
                   var v_rc = t.GetBound();
                   CoreUnit b = new CoreUnit();
                   float v_left = ((b = t.GetValue<CoreUnit>(Core2DMarginDependency.LeftProperty)) !=null) ? b.GetPixel(): 0;
                   float v_top = ((b = t.GetValue<CoreUnit>(Core2DMarginDependency.TopProperty)) != null) ? b.GetPixel() : 0; 
                   float v_right = ((b = t.GetValue<CoreUnit>(Core2DMarginDependency.RightProperty)) != null) ? b.GetPixel() : 0; 
                   float v_bottom = ((b = t.GetValue<CoreUnit>(Core2DMarginDependency.BottomProperty)) != null) ? b.GetPixel() : 0;

                   Colorf cl = Colorf.DeepSkyBlue;
                   Colorf bordercl = Colorf.DarkBlue;
                   Colorf fillcl = Colorf.WhiteSmoke;
                   var v_docBound = this.CurrentSurface.CurrentDocument.Bounds;
                   var brc = this.GetClientRectangle(snippet);
                   brc.Inflate(inflate, inflate);
                   switch (snippet.Index)
                   {
                       case 0:
                           if (v_left != 0)
                           {
                               s = CurrentSurface.GetScreenLocation(new Vector2f(v_left, v_rc.MiddleLeft.Y));
                               device.DrawLine(cl,
                                   s,
                                   CurrentSurface.GetScreenLocation(new Vector2f(0, v_rc.MiddleLeft.Y))
                                   );
                               __drawSnippet(device, s, fillcl, bordercl, brc);
                           }
                           break;
                       case 1:
                           if (v_top != 0)
                           {
                               s = CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, v_top));
                               device.DrawLine(cl,
                                  s,
                                   CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, 0))
                                   );
                               __drawSnippet(device, s, fillcl, bordercl, brc);
                           }
                           break;
                       case 2:
                           //Right
                           if (v_right != 0)
                           {
                               s = CurrentSurface.GetScreenLocation(new Vector2f(v_docBound.Right - v_right, v_rc.Center.Y));
                               device.DrawLine(cl,
                                   s,
                                  CurrentSurface.GetScreenLocation(new Vector2f(v_docBound.Right, v_rc.Center.Y)));
                               __drawSnippet(device, s, fillcl, bordercl, brc);
                           }
                           break;
                       case 3:
                           //bottom
                           if (v_bottom != 0)
                           {
                               s = CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, v_docBound.Bottom - v_bottom));
                               device.DrawLine(cl,
                                   s,
                                   CurrentSurface.GetScreenLocation(new Vector2f(v_rc.Center.X, v_docBound.Bottom)));
                               __drawSnippet(device, s, fillcl, bordercl, brc);
                           }
                           break;
                   }
                   device.Restore(p);
               }
               private void __drawSnippet(ICoreGraphics device, Vector2f s, Colorf fillcl, Colorf bordercl, Rectanglef brc)
               {
                   device.FillRectangle(fillcl, brc);
                   device.DrawRectangle(bordercl, brc);
                   
               }
               public override void Render(ICoreGraphics device)
               {
                   var r = this.Element;
                   if (r == null)
                       return;
                   var m = device.Save();
                   float x =0;
                   float y =0;
                   float w =0;
                   float h =0;
                   this.ApplyCurrentSurfaceTransform(device);
                   device.FillRectangle(Colorf.WhiteSmoke, new Rectanglef(x, y, w, h));
                   device.DrawRectangle(Colorf.DeepSkyBlue, x, y, w, h);
                   device.Restore(m);
               }

               public Rectanglef GetClientRectangle(ICoreSnippet snippet)
               {
                   Rectanglef brc = new Rectanglef(snippet.Location, Size2f.Empty);
                   switch (snippet.Index)
                   {
                       case 0:
                       case 2:
                           brc.Inflate(2,8);
                           break;
                       case 1:
                       case 3:
                           brc.Inflate(8, 2);
                           break;
                       default:
                           break;
                   }
                   return brc;
               }


               protected override void OnMouseUp(CoreMouseEventArgs e)
               {
                   base.OnMouseUp(e);
               }
               protected override void OnMouseDown(CoreMouseEventArgs e)
               {
                   switch (e.Button)
                   {
                       case enuMouseButtons.Left:
                           if (this.Element == null)
                           {
                               this.SelectOne(e.FactorPoint);
                               if (this.Element != null)
                               {
                                   this.ResetAll();
                               }
                               this.GenerateSnippets();
                               this.InitSnippetsLocation();
                               this.EnabledSnippet();
                               return;
                           }
                           else {
                               if (this.Element.Contains(e.FactorPoint))
                               {
                                   BeginMove(e);
                                   return;
                               }
                           }
                           break;
                       case enuMouseButtons.Right:
                       default:
                           break;
                   }
                
                   base.OnMouseDown(e);
               }
               protected override void OnMouseMove(CoreMouseEventArgs e)
               {
                   switch (e.Button)
                   {
                       case enuMouseButtons.Left:
                           switch (this.State)
	                        {
                               case ST_MOVING :
                                    UpdateMove (e);
                                    break ;
                                default:
                                    break;
	                        }
                           break;
                   }
                   base.OnMouseMove(e);
               }
           }
    }
}
