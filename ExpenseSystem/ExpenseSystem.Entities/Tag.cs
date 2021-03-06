//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ExpenseSystem.Entities
{
    public partial class Tag
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<ExpenseRecord> ExpenseRecords
        {
            get
            {
                if (_expenseRecords == null)
                {
                    var newCollection = new FixupCollection<ExpenseRecord>();
                    newCollection.CollectionChanged += FixupExpenseRecords;
                    _expenseRecords = newCollection;
                }
                return _expenseRecords;
            }
            set
            {
                if (!ReferenceEquals(_expenseRecords, value))
                {
                    var previousValue = _expenseRecords as FixupCollection<ExpenseRecord>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupExpenseRecords;
                    }
                    _expenseRecords = value;
                    var newValue = value as FixupCollection<ExpenseRecord>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupExpenseRecords;
                    }
                }
            }
        }
        private ICollection<ExpenseRecord> _expenseRecords;
    
        public virtual ICollection<Tag> Children
        {
            get
            {
                if (_children == null)
                {
                    var newCollection = new FixupCollection<Tag>();
                    newCollection.CollectionChanged += FixupChildren;
                    _children = newCollection;
                }
                return _children;
            }
            set
            {
                if (!ReferenceEquals(_children, value))
                {
                    var previousValue = _children as FixupCollection<Tag>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupChildren;
                    }
                    _children = value;
                    var newValue = value as FixupCollection<Tag>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupChildren;
                    }
                }
            }
        }
        private ICollection<Tag> _children;
    
        public virtual Tag Parent
        {
            get { return _parent; }
            set
            {
                if (!ReferenceEquals(_parent, value))
                {
                    var previousValue = _parent;
                    _parent = value;
                    FixupParent(previousValue);
                }
            }
        }
        private Tag _parent;
    
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;

        #endregion
        #region Association Fixup
    
        private void FixupParent(Tag previousValue)
        {
            if (previousValue != null && previousValue.Children.Contains(this))
            {
                previousValue.Children.Remove(this);
            }
    
            if (Parent != null)
            {
                if (!Parent.Children.Contains(this))
                {
                    Parent.Children.Add(this);
                }
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.Tags.Contains(this))
            {
                previousValue.Tags.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.Tags.Contains(this))
                {
                    User.Tags.Add(this);
                }
            }
        }
    
        private void FixupExpenseRecords(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ExpenseRecord item in e.NewItems)
                {
                    item.Tag = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ExpenseRecord item in e.OldItems)
                {
                    if (ReferenceEquals(item.Tag, this))
                    {
                        item.Tag = null;
                    }
                }
            }
        }
    
        private void FixupChildren(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Tag item in e.NewItems)
                {
                    item.Parent = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Tag item in e.OldItems)
                {
                    if (ReferenceEquals(item.Parent, this))
                    {
                        item.Parent = null;
                    }
                }
            }
        }

        #endregion
    }
}
