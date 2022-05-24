namespace RayTracer.Strategies
{
    interface IParserStrategy
    {
        public bool CanHandle(string entityName);
        
        public void AddObjectToContext(ObjectContext context, string[] entity);
    }
}
