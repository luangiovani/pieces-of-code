using AutoMapper;

namespace Framework.Domain.AutoMapper
{
  public class AutoMapperConfig
  {
    public static void RegisterMappings()
    {
      Mapper.Initialize(x =>
      {
        x.AddProfile<DomainToViewModel>();
        x.AddProfile<ViewModelToDomain>();
      });
    }
  }

  public static class AutoMapperExtensions
  {
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
      foreach (var property in typeof(TDestination).GetProperties())
      {
        if (property.GetGetMethod().IsVirtual)
          expression.ForMember(property.Name, opt => opt.Ignore());
      }

      //foreach (var property in expression.TypeMap.GetUnmappedPropertyNames())
      //{
      //    expression.ForMember(property, opt => opt.Ignore());
      //}
      return expression;
    }
  }
}