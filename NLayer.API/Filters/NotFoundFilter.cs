using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters;

public class NotFoundFilter<T> :IAsyncActionFilter where T :BaseEntity
{
    private readonly IService<T> _service;

    /*
     * Eğer bir filter cons tarafında bir servisi bir sınıfı implement ediyorsa
     * bunu startup tarafında göstermemiz gerekiyor.
     */
    public NotFoundFilter(IService<T> service)
    {
        _service = service;
    }

    public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        /*
         * Biz bunu getbyId endpoind'inde kullanmak üzere tasarlıyoruz. Bu sebeple ilk olarak
         * parametre olarak gelen id değerini almamız lazım.
         * Bu filter olayında en önemli nesne context nesnesi burada kalıtımla alınıyor ve kullanılıyor.
        */
        var idValue = context.ActionArguments.Values.FirstOrDefault();
        //bu object olarak bizim actionmetota istek yapıldığında oradaki parametreleri almamızı sağlayan yapıdır.
        //values yapısı Tkey , Tvalue ikilisi olan bir dizidir. FirstorDefault harici key ile de değeri alabiliriz.

        if (idValue == null) //eger endpoint'de bir id gelmiyor ise devam et diyeceğiz
        {
            await next.Invoke();
            return;
            
        }

        var id = (int)idValue; //=>casting for int

        var anyT = await  _service.AnyAsync(x=>x.Id == id);
        if (anyT)
        {
            await next.Invoke();
            return;
        }

        context.Result = new NotFoundObjectResult(CustomReponseDto<NoContentDto>.Fail(404,$"{typeof(T).Name} ({id} not found )"));

    }
}