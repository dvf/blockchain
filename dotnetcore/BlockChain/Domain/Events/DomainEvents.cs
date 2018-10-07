namespace BlockChain.Domain.Events
{
    public static class DomainEvents
    {
        public static void Raise<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            // todo:
            // see: http://www.udidahan.com/2009/06/14/domain-events-salvation/
            // and: http://lostechies.com/jimmybogard/2010/04/08/strengthening-your-domain-domain-events/
        }
    }
}