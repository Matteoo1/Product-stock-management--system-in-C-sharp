protected override void OnPaint(PaintEventArgs e)
{
    GraphicsPath grPath = new GraphicsPath();

    // Subtract a portion from width and height to make the ellipse smaller
    int margin = 100; // Adjust this value to control the size of the margin
    int width = ClientRectangle.Width - margin * 2;
    int height = ClientRectangle.Height - margin * 2;

    // Create ellipse using the adjusted width and height
    grPath.AddEllipse(ClientRectangle.Left + margin, ClientRectangle.Top + margin, width, height);

    // Set the region of the button to the ellipse path
    this.Region = new Region(grPath);

    // Draw the button
    base.OnPaint(e);
}
