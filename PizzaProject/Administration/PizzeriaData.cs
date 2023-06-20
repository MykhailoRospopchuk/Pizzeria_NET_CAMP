﻿using PizzaProject.Costumer_Payment.CashRegisters;
using PizzaProject.Costumer_Payment.People;
using PizzaProject.Menu_Loyality.Loyality;
using PizzaProject.Menu_Loyality.Menu;
using PizzaProject.Storage_Waiter.Interfaces;
using PizzaProject.Storage_Waiter.Staff;

namespace PizzaProject.Administration
{
    public class PizzeriaData
    {
        private Manager _manager;
        private HashSet<IStaff> _staff;
        private HashSet<Waiter> _waiters;
        private ChefManager _chefManager;
        //public static ChefManager ChefManager { get; set; } = new ChefManager("Muhammad");

        private LoyaltyProgram _loyaltyProgram;
        private Menu _menu;
        private HashSet<ICashRegister> _cashRegs;

        public Storage ProductStorage { get; private set; }

        //public static Storage Storage { get; set; } = new Storage();

        public PizzeriaData(HashSet<Waiter> waiters, HashSet<IStaff> staff, Manager manager, ChefManager chefManager, LoyaltyProgram loyaltyProgram, Menu menu, HashSet<ICashRegister> cashRegs, Storage productStorage)
        {
            _waiters = waiters;
            _chefManager = chefManager;
            _staff = staff;

            _manager = manager;
            

            _loyaltyProgram = loyaltyProgram;
            _menu = menu;

            _cashRegs = cashRegs;

            ProductStorage = productStorage;

        }

        public Manager Manager { get => _manager; }
        public IEnumerable<IStaff> Staff { get => _staff; }
        public ChefManager ChefManager { get => _chefManager; }
        public LoyaltyProgram LoyaltyProgram { get => _loyaltyProgram; }
        public Menu Menu { get => _menu; }

        public HashSet<ICashRegister> CashRegs { get => _cashRegs; }

        public void RevisionStorage()
        {
            ProductStorage.RequestIngredient();
        }


        public string GetStaffInfo(IStaff staff)
        {
            return staff.Info;
        }

        public void SetVipStatus(Customer customer, HashSet<VipLvl> status)
        {
            customer.SetVipLvls(status);
        }

        public void AddStaff(IStaff staff)
        {
            if (staff != null && !(staff is Manager) && !(staff is ChefManager))
            {
                _staff.Add(staff);

                

                if (staff is Chef chef)
                {
                    _chefManager.Chefs.Add(chef);
                    ConnectorHandler.NewChefAddNotify(chef);
                }
                
            }
        }

        public void RemoveStaff(IStaff staff)
        {
            _staff.Remove(staff);
        }

        public void AddICashRegister(ICashRegister cashRegister)
        {
            if (cashRegister != null)
            {
                _cashRegs.Add(cashRegister);
            }
        }

        public void RemoveICashRegister(ICashRegister cashRegister)
        {
            _cashRegs.Remove(cashRegister);
        }

        public void SetManager(IPerson person)
        {
            if (person != null)
            {
                Manager manager = new Manager(person.Id, person.Name, _menu);
                _manager = manager;
            }
        }

        public void AddWaiter(Waiter waiter)
        {
            _waiters.Add(waiter);
            waiter.StartWorking();
            ConnectorHandler.SendWaiterData(waiter);
            ConnectorHandler.NewWaiterAddNotify(waiter);

        }
        public void StartWaiter()
        {
            foreach (var item in _waiters)
            {
                item.StartWorking();
                ConnectorHandler.SendWaiterData(item);
            }
        }


        public enum VipLvl
        {
            None,
            Bronze,
            Silver,
            Gold
        }
        public enum Category
        {
            Drinks,
            Pizzas,
            Sweets
        }
    }
}
