using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Storage;
using Sulli.Game.Storage.Layer;

namespace Sulli.Game.Mock
{
    public class StudentPipe : StoragePipe<Student, int>
    {
        public StudentPipe()
        {
            MemoryLayerOption memoryLayerOption = new MemoryLayerOption();
            MemoryLayer<Student, int> memoryLayer = new MemoryLayer<Student, int>("memory", memoryLayerOption);

            MySqlLayerOption mysqlLayerOption = new MySqlLayerOption();
            MySqlLayer<Student, int> mySqlLayer = new MySqlLayer<Student, int>("mysql", mysqlLayerOption);

            this.Layers.Add(memoryLayer);
            this.Layers.Add(mySqlLayer);
        }
    }
}
