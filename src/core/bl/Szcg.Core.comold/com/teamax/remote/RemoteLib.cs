using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
namespace DemoRemoteObj
{
	
	public class AccountArgs:EventArgs
	{
		public DateTime Dt;
		public string Place;
		

		public AccountArgs(DateTime dt,string place)
		{
			this.Dt = dt;
			this.Place = place;
		}
	}

	public delegate void AccountHandler(object sender,AccountArgs args);
	
	public class Account:MarshalByRefObject
	{
		
		public event AccountHandler Account_Changed = null;

		public decimal balance = 1000M;
		public void withdraw(decimal amount)
		{

			this.balance -= amount;
			AccountArgs args = new AccountArgs(System.DateTime.Now,"Plaza");
			this.onAccoutChanged(args);

		}

		protected void onAccoutChanged(AccountArgs args)
		{
			if(this.Account_Changed != null)
			{
				this.Account_Changed(this,args);
			}


		}
	}
	
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class RemoteLib:MarshalByRefObject
	{
		public override object InitializeLifetimeService()
		{
			ILease lease = base.InitializeLifetimeService() as ILease;
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromSeconds(30);
				lease.SponsorshipTimeout = TimeSpan.FromSeconds(20);
				lease.RenewOnCallTime = TimeSpan.FromSeconds(20);
			}
			return lease;
		}

		public string  getDomainName()
		{
			return System.Threading.Thread.GetDomain().FriendlyName;
		}

		public RemoteAuthorInfo getAuthorInfo(string name,string address)
		{
			return new RemoteAuthorInfo(name,address);
		}
		
		public string getHostName()
		{
			return System.Net.Dns.GetHostName();
		}

	}
}
