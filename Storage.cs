// Current User Entity

public class CurrentUser 
{
        [Key]
        public int ID { get; set; }
        public long? UserID { get; set; }
}

// Current User Service 
using LinqKit;
using StorexWebCore.Enities;
using StorexWebRepository;


namespace StorexWebService
{
    public interface ICurrentUserService
    {
        CurrentUser? Search();
        bool Insert(CurrentUser vm);
        bool Delete(CurrentUser vm);
    }
    public class CurrentUserService : ICurrentUserService
    {
        private IStorexWebRepository<CurrentUser> _CurrentUserRepo = null;
        public CurrentUserService()
        {
            _CurrentUserRepo = new StorexWebRepository<CurrentUser>();
        }

        public bool Insert(CurrentUser model)
        {
            bool success = _CurrentUserRepo.Insert(model);
            return success;
        }

        public bool Delete(CurrentUser model)
        {
            return _CurrentUserRepo.Delete(model);
        }

        public CurrentUser? Search()
        {
            var predicate = PredicateBuilder.New<CurrentUser>(true);

            predicate = predicate.And(p => p.ID == 1);  // ID --> 1
            

            return  _CurrentUserRepo.Table.AsExpandable().Where(predicate).FirstOrDefault();
        }
    }
}


// implementation Two Method  ( SaveCurrentUser , GetCurrentUser )


public void SaveCurrentUser(long UserID)
{
     CurrentUser CurrentUser = new CurrentUser();
     ICurrentUserService ICurrentUserService = new CurrentUserService();
     CurrentUser? DeleteUser = ICurrentUserService.Search();
     if (DeleteUser != null)
     {
          ICurrentUserService.Delete(DeleteUser);
     }
     ICurrentUserService.Insert(new CurrentUser() { UserID = UserID, ID = 1 });
}

public long? GetCurrentUser()
{
     ICurrentUserService ICurrentUserService = new CurrentUserService();
     CurrentUser? CurrentUser = ICurrentUserService.Search();
      if(CurrentUser != null)
        return CurrentUser.UserID;
     return null;
}
