namespace RL.Entity
{
    public class BaseProperties
    {
        public BaseProperties()
        {
            LogDate = DateTime.Now;
        }

        public int Id { get; set; } = default;
        public DateTime LogDate { get; set; } = default;
    }
}
