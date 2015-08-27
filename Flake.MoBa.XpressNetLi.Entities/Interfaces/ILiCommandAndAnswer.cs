namespace Flake.MoBa.XpressNetLi.Entities.Interfaces
{
    public interface ILiCommandAndAnswer
    {
        ILiCommunication Answer { get; set; }
        ILiCommunication Command { get; set; }
    }
}