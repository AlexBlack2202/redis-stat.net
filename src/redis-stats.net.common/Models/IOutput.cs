namespace redis_stat.net.common.Models
{
    public interface IOutput
    {
        void Write(Stats stats);
    }
}
