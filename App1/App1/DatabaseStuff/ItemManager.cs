using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace App1.DatabaseStuff
{
    public partial class ItemManager
    {
        static ItemManager defaultInstance = new ItemManager();
        MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<TodoItem> todoTable;
#else
        IMobileServiceTable<Patient_Table> patient_Table;
        IMobileServiceTable<Relative_Table> relative_Table;
        IMobileServiceTable<Hobby_Table> hobby_Table;
        IMobileServiceTable<Fear_Table> fear_Table;
#endif

        const string offlineDbPath = @"localstore.db";

        private ItemManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<TodoItem>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.patient_Table = client.GetSyncTable<TodoItem>();
#else
            this.patient_Table = client.GetTable<Patient_Table>();
            this.relative_Table = client.GetTable<Relative_Table>();
            this.hobby_Table = client.GetTable<Hobby_Table>();
            this.fear_Table = client.GetTable<Fear_Table>();
#endif
        }

        public static ItemManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task SaveTaskAsyncPatient(Patient_Table item)
        {
            if (item.Id == null)
            {
                await patient_Table.InsertAsync(item);
            }
            else
            {
                await patient_Table.UpdateAsync(item);
            }
        }

        public async Task SaveTaskAsyncRelative(Relative_Table item)
        {
            if (item.Id == null)
            {
                await relative_Table.InsertAsync(item);
            }
            else
            {
                await relative_Table.UpdateAsync(item);
            }
        }

        public async Task SaveTaskAsyncHobby(Hobby_Table item)
        {
            if (item.Id == null)
            {
                await hobby_Table.InsertAsync(item);
            }
            else
            {
                await hobby_Table.UpdateAsync(item);
            }
        }

        public async Task SaveTaskAsyncFear(Fear_Table item)
        {
            if (item.Id == null)
            {
                await fear_Table.InsertAsync(item);
            }
            else
            {
                await fear_Table.UpdateAsync(item);
            }
        }
        
        public async Task<ObservableCollection<Relative_Table>> GetRelativeItemsAsync(String currentUserId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<Relative_Table> items = await relative_Table
                    .Where(relativeItem => relativeItem.PatientID_FK == currentUserId)
                    .ToEnumerableAsync();

                return new ObservableCollection<Relative_Table>(items);
        }

        public async Task<ObservableCollection<Hobby_Table>> GetHobbyItemsAsync(String currentUserId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Hobby_Table> items = await hobby_Table
                .Where(hobbyItem => hobbyItem.PatientID_FK == currentUserId)
                .ToEnumerableAsync();

            return new ObservableCollection<Hobby_Table>(items);
        }

        public async Task<ObservableCollection<Fear_Table>> GetFearItemsAsync(String currentUserId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Fear_Table> items = await fear_Table
                .Where(fearItem => fearItem.PatientID_FK == currentUserId)
                .ToEnumerableAsync();

            return new ObservableCollection<Fear_Table>(items);
        }
    }
}
