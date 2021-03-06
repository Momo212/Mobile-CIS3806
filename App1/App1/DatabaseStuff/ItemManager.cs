﻿using System;
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
        IMobileServiceTable<Patient_History> patient_History;
        IMobileServiceTable<Patient_Alarm_Table> patient_Alarm_Table;
        IMobileServiceTable<Danger_Table> danger_Table;
        IMobileServiceTable<LUT_Alarm_Danger_Category> lut_alarm_danger_table;
        IMobileServiceTable<Alarm_Table> alarm_Table;
        IMobileServiceTable<Coordinate_Table> coordinate_Table;
        IMobileServiceTable<DangerActual_Table> dangerActual_Table;
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
            this.patient_History = client.GetTable<Patient_History>();
            this.patient_Alarm_Table = client.GetTable<Patient_Alarm_Table>();
            this.danger_Table = client.GetTable<Danger_Table>();
            this.alarm_Table = client.GetTable<Alarm_Table>();
            this.lut_alarm_danger_table = client.GetTable<LUT_Alarm_Danger_Category>();
            this.coordinate_Table = client.GetTable<Coordinate_Table>();
            this.dangerActual_Table = client.GetTable<DangerActual_Table>();
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

        public async Task SaveTaskAsyncCoordinate(Coordinate_Table item)
        {
            if (item.Id == null)
            {
                await coordinate_Table.InsertAsync(item);
            }
            else
            {
                await coordinate_Table.UpdateAsync(item);
            }
        }

        public async Task SaveTaskAsyncPatientMedical(Patient_History item)
        {
            if (item.Id == null)
            {
                await patient_History.InsertAsync(item);
            }
            else
            {
                await patient_History.UpdateAsync(item);
            }
        }

        public async Task SaveTaskAsyncDangerActualItem(DangerActual_Table item)
        {
            if (item.Id == null)
            {
                await dangerActual_Table.InsertAsync(item);
            }
            else
            {
                await dangerActual_Table.UpdateAsync(item);
            }
        }

        public async Task<ObservableCollection<DangerActual_Table>> GetDangerActualItemsAsync(String currentUserId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<DangerActual_Table> items = await dangerActual_Table
                .Where(dangerItem => dangerItem.PatientID_FK == currentUserId)
                .ToEnumerableAsync();

            return new ObservableCollection<DangerActual_Table>(items);
        }
        public async Task<ObservableCollection<Patient_Table>> GetPatientIDAsync()
        {
#if OFFLINE_SYNC_ENABLED//din zidta biex ngib il patients
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Patient_Table> items = await patient_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Patient_Table>(items);
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

        public async Task<ObservableCollection<Patient_Table>> GetPatient(string patientID)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Patient_Table> items = await patient_Table
                .Where(pat => pat.Patient_ID == patientID)
                .ToEnumerableAsync();

            return new ObservableCollection<Patient_Table>(items);
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

        public async Task<ObservableCollection<Hobby_Table>> GetHobbyItemsCountAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Hobby_Table> items = await hobby_Table
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

        public async Task<ObservableCollection<Fear_Table>> GetFearItemsCountAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Fear_Table> items = await fear_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Fear_Table>(items);
        }

        public async Task<ObservableCollection<Patient_History>> GetHistoryItemsAsync(String currentUserId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Patient_History> items = await patient_History
                .Where(historyItem => historyItem.PatientID_FK == currentUserId)
                .ToEnumerableAsync();

            return new ObservableCollection<Patient_History>(items);
        }

        public async Task<ObservableCollection<Patient_Alarm_Table>> GetPatientAlarmTableItemsAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Patient_Alarm_Table> items = await patient_Alarm_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Patient_Alarm_Table>(items);
        }

        public async Task<ObservableCollection<Danger_Table>> GetDangerTableItemsAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Danger_Table> items = await danger_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Danger_Table>(items);
        }

        public async Task<ObservableCollection<Alarm_Table>> GetAlarmTableItemsAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Alarm_Table> items = await alarm_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Alarm_Table>(items);
        }

        public async Task<ObservableCollection<LUT_Alarm_Danger_Category>> GetLUT_Alarm_Danger_CategoryTableItemsAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<LUT_Alarm_Danger_Category> items = await lut_alarm_danger_table
                .ToEnumerableAsync();

            return new ObservableCollection<LUT_Alarm_Danger_Category>(items);
        }

        public async Task<ObservableCollection<Coordinate_Table>> GetCoordinateItemsAsync(String patientId)
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Coordinate_Table> items = await coordinate_Table
                .Where(x => x.PatientID == patientId)
                .ToEnumerableAsync();

            return new ObservableCollection<Coordinate_Table>(items);
        }

        public async Task<ObservableCollection<Patient_Table>> GetPatientItemsAsync()
        {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
            IEnumerable<Patient_Table> items = await patient_Table
                .ToEnumerableAsync();

            return new ObservableCollection<Patient_Table>(items);
        }

    }
}
