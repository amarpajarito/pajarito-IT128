using System.Collections.Generic;

namespace BlogDataLibrary.Database
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatment, U parameters, string connectionStringName, bool isStoredProcedure);
        void SaveData<T>(string sqlStatment, T parameters, string connectionStringName, bool isStoredProcedure);
    }
}