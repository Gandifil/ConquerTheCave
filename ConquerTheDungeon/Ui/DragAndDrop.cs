using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Misc;
using MLEM.Ui;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class DragAndDrop: CardImage
{
    private readonly Vector2 _clickPoint;
    
    public DragAndDrop(Vector2 displayAreaSize, Vector2 clickPoint) : base(Anchor.TopLeft, displayAreaSize, false)
    {
        _clickPoint = clickPoint;
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
        OnRemovedFromUi += element => { Game1.Instance.Mouse.MouseDragEnd -= MouseOnMouseDragEnd; };
        SyncWithMouse();
    }

    private void MouseOnMouseDragEnd(object sender, MouseEventArgs e)
    {
        if (this.Root.Element == this)
            this.System.Remove(this.Root.Name);
        else
            this.Parent.RemoveChild(this);
    }

    public override void Update(GameTime time)
    {
        SyncWithMouse();

        base.Update(time);
    }

    private void SyncWithMouse()
    {
        var state = Mouse.GetState();
        PositionOffset = state.Position.ToVector2() - _clickPoint;
    }
}