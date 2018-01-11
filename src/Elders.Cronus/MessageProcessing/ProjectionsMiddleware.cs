namespace Elders.Cronus.MessageProcessing
{
    public class ProjectionsMiddleware : MessageHandlerMiddleware
    {
        public ProjectionsMiddleware(IHandlerFactory factory) : base(factory) { }
    }

    public class ClusterMiddleware : MessageHandlerMiddleware
    {
        public ClusterMiddleware(IHandlerFactory factory) : base(factory) { }
    }
}
