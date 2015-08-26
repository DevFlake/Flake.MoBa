using System;

namespace Flake.MoBa.XpressNetLi.Base.Enums
{
#pragma warning disable 0067

    /// <summary>
    /// extension of enum objects by cbo data binding options
    /// </summary>
    /// <typeparam name="T">enum type</typeparam>
    public abstract class EnumExtendedBase<T>
    {
        protected T _Value;

        protected abstract T NoneValue();

        protected abstract string Caption();

        #region "constructors"

        /// <summary>
        /// constructor taking enum as value
        /// </summary>
        /// <param name="enumValue">value of enum</param>
        protected EnumExtendedBase(T enumValue)
        {
            _Value = enumValue;
        }

        /// <summary>
        /// constructor taking string as value
        /// </summary>
        /// <param name="value">enum string</param>
        /// <remarks>a value not in enum results a noneValue</remarks>
        public EnumExtendedBase(string value)
        {
            try
            {
                _Value = (T)Enum.Parse(_Value.GetType(), value, true);
            }
            catch
            {
                _Value = this.NoneValue();
            }
        }

        /// <summary>
        /// constructor taking int as value
        /// </summary>
        /// <param name="value">enum int</param>
        /// <remarks>a value not in enum results a noneValue</remarks>
        public EnumExtendedBase(int value)
        {
            NumValue = value;
        }

        #endregion "constructors"

        #region "value properties"

        /// <summary>
        /// eunm value
        /// </summary>
        public T EnumValue
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// value as string
        /// </summary>
        public string StringValue
        {
            get { return Enum.GetName(_Value.GetType(), _Value); }
            set
            {
                if (Enum.IsDefined(_Value.GetType(), value))
                {
                    _Value = (T)Enum.Parse(_Value.GetType(), Convert.ToString(value));
                }
                else
                {
                    _Value = this.NoneValue();
                }
            }
        }

        /// <summary>
        /// value as string or null
        /// </summary>
        public object StringValue_OrNull
        {
            get
            {
                try
                {
                    if (_Value.Equals(this.NoneValue()))
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        return Enum.GetName(_Value.GetType(), _Value);
                    }
                }
                catch
                {
                    return DBNull.Value;
                }
            }
            set
            {
                try
                {
                    if ((value) is DBNull)
                    {
                        _Value = this.NoneValue();
                    }
                    else
                    {
                        if (Enum.IsDefined(_Value.GetType(), value))
                        {
                            _Value = (T)Enum.Parse(_Value.GetType(), Convert.ToString(value));
                        }
                        else
                        {
                            _Value = this.NoneValue();
                        }
                    }
                }
                catch
                {
                    _Value = this.NoneValue();
                }
            }
        }

        /// <summary>
        /// value as int
        /// </summary>
        public int NumValue
        {
            get { return (int)(object)_Value; }
            set
            {
                if (Enum.IsDefined(_Value.GetType(), value))
                {
                    _Value = (T)(object)value;
                }
                else
                {
                    _Value = this.NoneValue();
                }
            }
        }

        #endregion "value properties"

        #region "other"

        /// <summary>
        /// Name as text
        /// </summary>
        public string Name
        { get { return Caption(); } }

        /// <summary>
        /// dsiplay member for databinding
        /// </summary>
        /// <returns></returns>
        public static string DisplayMember()
        {
            return "Name";
        }

        /// <summary>
        /// value member for databinding
        /// </summary>
        /// <returns></returns>
        public static string ValueMember()
        {
            return "StringValue_OrNull";
        }

        /// <summary>
        /// get list of all enum values
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static System.Collections.IList GetList(Type t)
        {
            MyList a = new MyList();

            foreach (T x in Enum.GetValues(typeof(T)))
            {
                object[] args = { x };
                a.Add(System.Activator.CreateInstance(t, args));
            }

            return a;
        }

        public class MyList : System.Collections.ArrayList, System.ComponentModel.IBindingList
        {
            public void AddIndex(System.ComponentModel.PropertyDescriptor property)
            {
                throw new NotImplementedException();
            }

            public object AddNew()
            {
                throw new NotImplementedException();
            }

            public bool AllowEdit
            {
                get { return false; }
            }

            public bool AllowNew
            {
                get { return false; }
            }

            public bool AllowRemove
            {
                get { return false; }
            }

            public void ApplySort(System.ComponentModel.PropertyDescriptor property, System.ComponentModel.ListSortDirection direction)
            {
                throw new NotImplementedException();
            }

            public int Find(System.ComponentModel.PropertyDescriptor property, object key)
            {
                if (key == null || key == DBNull.Value) return -1;

                System.Reflection.PropertyInfo prInfo = typeof(EnumExtendedBase<T>).GetProperty(property.Name);

                for (int i = 0; i < this.Count; i++)
                {
                    object propertyValue = prInfo.GetValue(this[i], null);
                    if (propertyValue.ToString() == key.ToString()) return i;
                }
                return -1;  // not found
            }

            public bool IsSorted
            {
                get { throw new NotImplementedException(); }
            }

            public event System.ComponentModel.ListChangedEventHandler ListChanged;

            public void RemoveIndex(System.ComponentModel.PropertyDescriptor property)
            {
                throw new NotImplementedException();
            }

            public void RemoveSort()
            {
                throw new NotImplementedException();
            }

            public System.ComponentModel.ListSortDirection SortDirection
            {
                get { throw new NotImplementedException(); }
            }

            public System.ComponentModel.PropertyDescriptor SortProperty
            {
                get { throw new NotImplementedException(); }
            }

            public bool SupportsChangeNotification
            {
                get { return false; }
            }

            public bool SupportsSearching
            {
                get { return true; }
            }

            public bool SupportsSorting
            {
                get { return false; }
            }
        }

        #endregion "other"
    }
}