using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface IDataLayer
    {
        IEnumerable<DesRecord> GetDesRecord(int offset = 0, int count = 10);
        IEnumerable<ResultLine> GetRecord(string aggregationType, int aggregationFilter);
        IEnumerable<DesRecord> GetRecordAll();
    }
}
