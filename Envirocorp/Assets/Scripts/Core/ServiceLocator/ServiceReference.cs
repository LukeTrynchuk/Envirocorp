using System;

namespace FireBullet.Core.Services
{
    /// <summary>
    /// A service reference is an object that holds
    /// a reference to a certain service. Other classes
    /// use references to access the functionality of
    /// a service without having strong coupeling with
    /// that specific implementation of a service.
    /// </summary>
	public class ServiceReference<T> where T: class
	{
		#region Public Variables
		public T Reference => ServiceLocator.GetService<T> ();
		#endregion

		#region Private Variables
		private T instance;
		#endregion

		#region Main Methods
		public void AddRegistrationHandle(Action Handle) => ServiceLocator.AddOnRegisterHandle<T> (Handle);
		
		public bool isRegistered() => (Reference != null);
		#endregion
	}
}
