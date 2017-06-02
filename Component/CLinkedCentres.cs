﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class CLinkedCentres : ModelData
    {
        private static CLinkedCentres _Instance = new CLinkedCentres();

        public CLinkedCentres()
      : base()
        {
        }

        public static CLinkedCentres Instance
        {
            get
            {
                return _Instance;
            }
        }

        public List<Centres> SearchCentresForUser(string IdentificationNumber)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            List<Centres> ObjectListCentres = new List<Centres>();
            try
            {
                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkUsers_Identifier == IdentificationNumber).ToList();

                foreach (var item in ObjectLinkedCentres)
                {
                    Centres ObjectCenter = CCentres.Instance.Centres.FirstOrDefault(c => c.PkIdentifier == item.FkCentres_Identifier);
                    ObjectListCentres.Add(ObjectCenter);
                }

                return ObjectListCentres;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCentresForUser" + "BGM" + ex.Message);

                return null;
            }
        }
        public List<Users> SearchUsersForCenter(string Identification)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            List<Users> ObjectListUsers = new List<Users>();
            try
            {
                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkCentres_Identifier == Identification).ToList();

                foreach (var item in ObjectLinkedCentres)
                {
                    Users ObjectUsers = CUsers.Instance.Users.FirstOrDefault(c => c.PkIdentifier == item.FkUsers_Identifier);
                    ObjectListUsers.Add(ObjectUsers);
                }

                return ObjectListUsers;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCentresForUser" + "BGM" + ex.Message);

                return null;
            }
        }

    }
}