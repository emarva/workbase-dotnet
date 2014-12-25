using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace WorkBase.Shared
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class AboutEditor : UITypeEditor
    {
        #region Overrides
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Acerca acerca = new Acerca();

            acerca.ShowDialog();

            acerca.Dispose();

            return null;
        }
        #endregion
    }
}
