using Repository;
using System;

namespace Model
{
    public class Repository:EFRepository.Repository, IDisposable, IRepository
    {
        public Repository(bool autoDetectChangesEnabled = false, bool proxiCreationEnabled = false) :
            base(new ControlHoraEntities(), autoDetectChangesEnabled, proxiCreationEnabled)
        {
        }

    }
}
