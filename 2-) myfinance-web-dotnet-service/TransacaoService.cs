using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;
using nyfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet_service;

public class TransacaoService : ITransacaoService
{
    public readonly MyFinanceDbContext _dbContext;
    public TransacaoService(MyFinanceDbContext dbContext) => _dbContext = dbContext;

        public void Cadastrar(Transacao Entidade)
    {

        var dbSet = _dbContext.Transacao;

        if(Entidade.Id == null){
            dbSet.Add(Entidade);
        }else{
            dbSet.Attach(Entidade);
            _dbContext.Entry(Entidade).State = EntityState.Modified;
        }

        _dbContext.SaveChanges();
    }

    public void Excluir(int Id)
    {
        var transacao = new Transacao(){Id = Id};
        _dbContext.Attach(transacao);
        _dbContext.Remove(transacao);
        _dbContext.SaveChanges();
    }

    public List<Transacao> ListarRegistros() =>
        _dbContext.Transacao.ToList();

    public Transacao RetornarRegistro(int Id) =>
        _dbContext.Transacao.First(p => p.Id == Id);

}
