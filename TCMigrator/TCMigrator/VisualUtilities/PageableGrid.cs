using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.VisualUtilities
{
    public class PageableGrid<T>:INotifyPropertyChanged
    {
        private int _pageSize = 12;
        public int PageSize { get
            {
                return _pageSize;
            }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    SendPropertyChanged(() => PageSize);
                    Reset();
                }
            }
        }
        public int PageCount
        {
            get
            {
                if (Records != null && PageSize > 0)
                {
                    return (Records.Count - 1) / PageSize + 1;
                }
                return 0;
            }
        }
        private int _pageNum = 1;
        public int PageNumber
        {
            get
            {
                return _pageNum;
            }
            protected set
            {
                if(_pageNum != value)
                {
                    _pageNum = value;
                    SendPropertyChanged(() => PageNumber);
                }
            }
        }
        private ObservableCollection<T> _currentRecords;
        public ObservableCollection<T> CurrentPageRecords
        {
            get { return _currentRecords; }
            private set
            {
                if(_currentRecords != value)
                {
                    _currentRecords = value;
                    SendPropertyChanged(() => CurrentPageRecords);
                }
            }
        }

        protected ObservableCollection<T> Records { get; set; }
        public ObservableCollection<T> AllRecords { get { return Records; } }

        protected PageableGrid()
        {

        }
        public PageableGrid(IEnumerable<T> records,int recordsPerPage=5)
        {
            Records = new ObservableCollection<T>(records);
            PageSize = recordsPerPage;
            Calculate(PageNumber);
        }

        public void Next()
        {
            if (PageNumber != PageCount)
            {
                PageNumber++;
                Calculate(PageNumber);
            }
        }
        public void Previous()
        {
            if (PageNumber > 1)
            {
                PageNumber--;
                Calculate(PageNumber);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)expression.Body).Member.Name));
            }
        }

        protected void Calculate(int pageNumber)
        {
            int ul = pageNumber * PageSize;
            CurrentPageRecords = 
                new ObservableCollection<T>(
                    Records.Where(x => Records.IndexOf(x) > ul - (PageSize + 1) && Records.IndexOf(x) < ul));
        }
        protected void Reset()
        {
            PageNumber = 1;
            Calculate(PageNumber);
            SendPropertyChanged(() => PageCount);
        }
    }
}
