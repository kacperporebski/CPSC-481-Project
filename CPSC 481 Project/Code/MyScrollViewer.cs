﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPSC_481_Project
{
  class MyScrollViewer : ScrollViewer
        {
            protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
            {
                base.OnPreviewMouseWheel(e);
                if (!e.Handled)
                {
                    e.Handled = true;
                    this.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                    {
                        RoutedEvent = UIElement.MouseWheelEvent,
                        Source = this
                    });
                }
            }
        }
    
}