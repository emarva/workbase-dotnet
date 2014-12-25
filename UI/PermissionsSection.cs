using System;
using System.Collections.Generic;
using System.Text;

namespace DevO2.UI
{
    [Serializable()]
    internal class PermissionsSection
    {
        private string propId;
        private string propDescripcion;

        public string Id
        {
            get { return this.propId; }
            set { this.propId = value; }
        }

        public string Descripcion
        {
            get { return this.propDescripcion; }
            set { this.propDescripcion = value; }
        }
    }

    [Serializable()]
    internal class PermissionsSectionCollection : System.Collections.CollectionBase
    {

    }
}
