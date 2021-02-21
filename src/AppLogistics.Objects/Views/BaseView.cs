using AppLogistics.Components.Extensions.Native;
using System;

namespace AppLogistics.Objects
{
    public abstract class BaseView
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual DateTime CreationDate
        {
            get
            {
                if (!IsCreationDateSet)
                {
                    CreationDate = DateTime.Now.UtcToDefaultTimeZone();
                }

                return InternalCreationDate;
            }
            protected set
            {
                IsCreationDateSet = true;
                InternalCreationDate = value;
            }
        }

        private bool IsCreationDateSet
        {
            get;
            set;
        }

        private DateTime InternalCreationDate
        {
            get;
            set;
        }
    }
}
