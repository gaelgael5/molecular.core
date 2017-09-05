using System;
using System.Collections.Generic;
using System.Text;

namespace Molecular.FileStore
{

    public class IndexModelList : DataModelBase
    {

        public IndexModelList()
        {
            this.Items = new List<IndexModel>();
        }

        public List<IndexModel> Items { get; set; }

    }

}
