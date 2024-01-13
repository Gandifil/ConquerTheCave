using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;
using MonoGame.Extended.Input.InputListeners;

namespace ConquerTheDungeon.Ui;

public class DragAndDrop: CardImage
{
    public DragAndDrop(Anchor anchor, Vector2 size) : base(anchor, size, false)
    {
        Game1.Instance.Mouse.MouseDragEnd += MouseOnMouseDragEnd;
        OnRemovedFromUi += element => { Game1.Instance.Mouse.MouseDragEnd -= MouseOnMouseDragEnd; };
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
        base.Update(time);
            
        var state = Mouse.GetState();
        PositionOffset = state.Position.ToVector2();
    }
}