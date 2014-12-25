using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DevO2.Shared;

namespace DevO2.UI
{
    public partial class ImageViewer : Component
    {
        public ImageViewer()
        {
            InitializeComponent();
        }

        [DesignOnly(true),
        EditorBrowsable(EditorBrowsableState.Never),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ParenthesizePropertyName(true),
        Editor(typeof(AboutEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public object Acerca { get { return null; } }

    }
}
