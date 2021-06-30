using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Framework.Database.Entity;
using Framework.Domain.Model;
using Framework.Domain.Repository;

namespace Framework.Domain.Services
{
    public class AreaService : ServiceBase<Area, AreaViewModel>
    {
        private readonly AreaRepository _repository;

        public AreaService(AreaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ICollection<AreaViewModel> GetByController(string controller)
        {
            return Mapper.Map<ICollection<AreaViewModel>>(_repository.Find(o => o.controller == controller));
        }

        public ICollection<AreaViewModel> GetMenuUsuario(string id_usuario)
        {
            return Mapper.Map<ICollection<AreaViewModel>>(_repository.GetMenuUsuario(id_usuario)).OrderBy(a => a.ordem).ToList();
        }

        public ICollection<AreaViewModel> GetEstruturado()
        {
            List<Area> final = new List<Area>();

            var areas = _repository.Find(o => o.id_area_mae == null).OrderBy(o => o.ordem);
            foreach (Area area in areas)
            {
                final.Add(area);
                GetAreaAux(area, final);
            }

            var retorno = final.Select(o =>
            {
                if (o.area_mae != null)
                    o.area_mae.nome = GetEstruturaMae(o.area_mae);

                return o;
            });

            return Mapper.Map<ICollection<AreaViewModel>>(retorno);
        }

        private void GetAreaAux(Area area, ICollection<Area> lista)
        {
            if (area.area_filha.Count > 0)
            {
                foreach (Area filha in area.area_filha.OrderBy(o => o.ordem))
                {
                    lista.Add(filha);
                    GetAreaAux(filha, lista);
                }
            }
        }

        private string GetEstruturaMae(Area area)
        {
            string retorno = "";

            if (area.area_mae != null)
                retorno += GetEstruturaMae(area.area_mae) + " .. ";

            return retorno + area.nome;
        }
    }
}