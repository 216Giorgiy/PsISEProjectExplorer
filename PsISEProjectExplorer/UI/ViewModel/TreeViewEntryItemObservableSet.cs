﻿using PsISEProjectExplorer.UI.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PsISEProjectExplorer.UI.ViewModel
{
    public class TreeViewEntryItemObservableSet : INotifyCollectionChanged, IEnumerable<TreeViewEntryItemModel>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private List<TreeViewEntryItemModel> Items { get; set; }

        public TreeViewEntryItemObservableSet()
        {
            this.Items = new List<TreeViewEntryItemModel>();
        }

        public bool Add(TreeViewEntryItemModel item)
        {
            int indexOfAddedItem = this.AddItem(item);
            if (indexOfAddedItem < 0)
            {
                return false;
            }
            this.RaiseOnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, indexOfAddedItem));
            return true;
        }

        public void Clear()
        {
            this.Items.Clear();
            this.RaiseOnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Remove(TreeViewEntryItemModel item)
        {
            int index = this.Items.BinarySearch(item, DefaultTreeViewEntryItemComparer.TreeViewEntryItemComparer);
            if (index < 0)
            {
                // not in list;
                return false;
            }
            this.Items.RemoveAt(index);
            this.RaiseOnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return true;
        }

        public IEnumerator<TreeViewEntryItemModel> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void RaiseOnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, e);
        }

        private int AddItem(TreeViewEntryItemModel item)
        {
            int result = this.Items.BinarySearch(item, DefaultTreeViewEntryItemComparer.TreeViewEntryItemComparer);
            if (result >= 0)
            {
                // already on list
                return -1;
            }
            int index = ~result;
            this.Items.Insert(index, item);
            return index;
        }
        
    }
}
