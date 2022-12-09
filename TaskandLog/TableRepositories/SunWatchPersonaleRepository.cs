using SQLite;
using TaskandLog.Database;
using TaskandLog.Tables;

namespace TaskandLog.TableRepositories
{
    public class SunWatchPersonaleRepository
    {
        readonly DB Database = new();

        public void Init()
        {
            Database.DatabaseConnection = Database.DatabaseInit();
            Database.DatabaseConnection.CreateTable<SunWatchPersonale>();
        }

        public void AddSunWatchPersonale(string SunWatch_personale_name, string SunWatch_personale_shift, string SunWatch_personale_days_worked)
        {
            Init();
            var sunwatchpersonale = new SunWatchPersonale
            {
                SunWatch_personale_name = SunWatch_personale_name,
                SunWatch_personale_shift=SunWatch_personale_shift,
                SunWatch_personale_days_worked= SunWatch_personale_days_worked

            };
            _ = Database.DatabaseConnection.Insert(sunwatchpersonale);
        }

        public void DeleteSunWatchPersonale(int SunWatch_personale_id)
        {
            Init();
            Database.DatabaseConnection.Delete<SunWatchPersonale>(SunWatch_personale_id);
        }

        public void UpdateSunWatchPersonale(string SunWatch_personale_knid, string SunWatch_personale_name, string SunWatch_personale_shift, string SunWatch_personale_days_worked)
        {
            Init();
            var sunwatchpersonale = new SunWatchPersonale
            {
                SunWatch_personale_knid = SunWatch_personale_knid,
                SunWatch_personale_name = SunWatch_personale_name,
                SunWatch_personale_shift = SunWatch_personale_shift,
                SunWatch_personale_days_worked= SunWatch_personale_days_worked

            };
            _ = Database.DatabaseConnection.Update(sunwatchpersonale);

        }

        public List<SunWatchPersonale> GetSunWatchPersonale()
        {
            Init();
            return Database.DatabaseConnection.Table<SunWatchPersonale>().ToList();
        }

        public TableQuery<SunWatchPersonale> QueryBySunWatchPersonaleName(string SunWatchPersonaleName)
        {
            Init();
            return Database.DatabaseConnection.Table<SunWatchPersonale>().Where(value => value.SunWatch_personale_name.Equals(SunWatchPersonaleName));

        }
    }
}
