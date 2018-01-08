using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App1
{
    class CusomMap : ContentView
    {
        private double startScale;
        private double currentScale;
        private double xOffset;
        private double yOffset;

        public CusomMap()
        {
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            MessagingCenter.Send(this, "LabelText", "Pinch");
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor.
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            MessagingCenter.Send(this, "LabelText", "Pan");
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX =
                      Math.Max(Math.Min(0, xOffset + e.TotalX), -Math.Abs(Content.Width));
                    Content.TranslationY =
                      Math.Max(Math.Min(0, yOffset + e.TotalY), -Math.Abs(Content.Height));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                    break;
            }
        }
    }
}
