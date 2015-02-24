using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsNum.Common {
    /// <summary>
    /// 可排序的 BindingList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T> {
        private bool isSorted;
        private PropertyDescriptor sortProperty;
        private ListSortDirection sortDirection;

        protected override bool IsSortedCore {
            get { return isSorted; }
        }

        protected override bool SupportsSortingCore {
            get { return true; }
        }

        protected override ListSortDirection SortDirectionCore {
            get { return sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore {
            get { return sortProperty; }
        }

        protected override bool SupportsSearchingCore {
            get { return true; }
        }

        public SortableBindingList(IList<T> list)
            : base(list) {
        }

        public SortableBindingList() : base() { }

        protected override void ApplySortCore(PropertyDescriptor prop , ListSortDirection direction) {
            List<T> items = this.Items as List<T>;

            if(items != null) {
                SortComparer<T> pc = new SortComparer<T>(prop , direction);
                items.Sort(pc);
                isSorted = true;
            } else {
                isSorted = false;
            }

            sortProperty = prop;
            sortDirection = direction;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset , -1));
        }

        protected override void RemoveSortCore() {
            isSorted = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset , -1));
        }
        //排序
        public void Sort(PropertyDescriptor property , ListSortDirection direction) {
            this.ApplySortCore(property , direction);
        }
    }

    internal class SortComparer<T> : IComparer<T> {
        private PropertyDescriptor m_PropDesc = null;
        private ListSortDirection m_Direction = ListSortDirection.Ascending;

        public SortComparer(PropertyDescriptor propDesc , ListSortDirection direction) {
            m_PropDesc = propDesc;
            m_Direction = direction;
        }

        int IComparer<T>.Compare(T x , T y) {
            object xValue = m_PropDesc.GetValue(x);
            object yValue = m_PropDesc.GetValue(y);
            return CompareValues(xValue , yValue , m_Direction);
        }

        private int CompareValues(object xValue , object yValue , ListSortDirection direction) {
            int retValue = 0;
            if(xValue == null && yValue == null) {
                retValue = 0;
            } else if(xValue is IComparable) { //can ask the x value  
                retValue = ((IComparable)xValue).CompareTo(yValue);
            } else if(yValue is IComparable) { //can ask the y value  
                retValue = ((IComparable)yValue).CompareTo(xValue);
            } else if(!xValue.Equals(yValue)) {
                //not comparable, compare string representations  
                retValue = xValue.ToString().CompareTo(yValue.ToString());
            }
            if(direction == ListSortDirection.Ascending)
                return retValue;
            else
                return retValue * -1;
        }
    }
}
