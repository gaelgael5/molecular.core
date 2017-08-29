﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchemaApi.Models.Generators
{

    public class ViewStep
    {
        private readonly List<KeyValuePair<string, StringBuilder>> _files;

        public ViewStep()
        {
            this._files = new List<KeyValuePair<string, System.Text.StringBuilder>>();
        }

        public string ViewName { get; set; }

        public string Target { get; set; }

        internal void AddFile(string filename, StringBuilder sb)
        {
            this._files.Add(new KeyValuePair<string, System.Text.StringBuilder>(filename, sb));
        }

        internal void Save(string path)
        {
            foreach (var item in this._files)
            {

                

                var filename = System.IO.Path.Combine(path, item.Key);
                FileInfo file = new FileInfo(filename);

                if (!file.Directory.Exists)
                    file.Directory.Create();

                using (var stream = file.Create())
                {
                    byte[] array = System.Text.Encoding.UTF8.GetBytes(item.Value.ToString());
                    stream.Write(array, 0, array.Length);
                }

            }
        }
    }

}