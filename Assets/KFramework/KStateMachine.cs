using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public abstract class KStateMachine<T> : MonoBehaviour
{   
	public T startState;
	public T currentState;
	 
	protected Dictionary<T, KState<T>> _availableStates;
	protected List<KState<T>>[] _priorityLevels;
	protected T[] _stateTypes;

	protected List<KState<T>> _currentActiveStates;
	public List<KState<T>> currentActiveStates
	{
		get { return _currentActiveStates; }
	}
	
	protected List<KState<T>> _lockedActiveStates;
	protected int _maxPriorityLevel = 0;

	public virtual void KInitialize()
	{	
		//Initialize lists
		_currentActiveStates = new List<KState<T>>();
		_lockedActiveStates = new List<KState<T>>();
		_availableStates = new Dictionary<T, KState<T>>();
		_priorityLevels = new List<KState<T>>[0];
		_stateTypes = new T[0];
		
		//Get all relative states on the scene
		_availableStates = new Dictionary<T, KState<T>>();
		KState<T>[] __statesFound;
		if(transform.FindChild("_states") != null)
		{
			__statesFound = transform.FindChild("_states").GetComponentsInChildren<KState<T>>();
			for (var i = 0; i < __statesFound.Length; i ++)
			{
				if(_availableStates.ContainsKey(__statesFound[i].state))
				{
					Debug.LogWarning("You can not have more than one GameObject with the same state identifier.\nThe behaviour " + __statesFound[i].state + "is asigned to more than one Object");
				}
				else
				{
					_availableStates.Add(__statesFound[i].state, __statesFound[i]);
				}
			}
			
			//Get state list
			_stateTypes = new T[_availableStates.Count];
			_availableStates.Keys.CopyTo(_stateTypes, 0);
			
			//Manage states
			for(var i = 0; i < _availableStates.Count; i ++)
			{
				//Get the max priority level
				if(_availableStates[_stateTypes[i]].priorityLevel > _maxPriorityLevel)
					_maxPriorityLevel = _availableStates[_stateTypes[i]].priorityLevel;
				
				//Define who is the manager
				_availableStates[_stateTypes[i]].manager = this;
				
				//Initialize State
				_availableStates[_stateTypes[i]].KInitialize();	
			}
			
			
			//Organize priority levels
			OrganizePriorityLevels();
			
			currentState = startState;
			ChangeToState(startState);
		}
		else
		{
			Debug.LogWarning("Unable to find states");
		}	
	}
	 
	private void OrganizePriorityLevels()
	{
		_priorityLevels = new List<KState<T>>[_maxPriorityLevel + 1];
		for(var i = 0; i < _priorityLevels.Length; i ++)
		{
			_priorityLevels[i] = new List<KState<T>>();
		}
		for(var i = 0; i < _availableStates.Count; i ++)
		{
			_priorityLevels[_availableStates[_stateTypes[i]].priorityLevel].Add(_availableStates[_stateTypes[i]]);
		}
	}
	
	/// <summary>
	/// This method is called every time a state is activated.
	/// </summary>
	/// <param name='p_activatedState'>
	/// The activated state.
	/// </param>
	protected virtual void OnStateActivated(KState<T> p_activatedState)
	{
	}
	
	/// <summary>
	/// This method is called every time a state is deactivated.
	/// </summary>
	/// <param name='p_deactivatedState'>
	/// The deactivated state.
	/// </param>
	protected virtual void OnStateDeactivated(KState<T> p_deactivatedState)
	{
	}
	
	/// <summary>
	/// Update all the states that are activated.
	/// </summary>
	/// <remarks>
	/// This method is called in every single frame, once per frame.
	/// </remarks>
	public virtual void KUpdate()
	{
		//Call all current active States' Update
		for(var i = 0; i < _priorityLevels.Length; i ++)
		{
			for(var j = 0; j < _priorityLevels[i].Count; j ++)
			{
				if(_currentActiveStates.Contains(_priorityLevels[i][j]))
					_priorityLevels[i][j].KUpdate();
			}
		}
	}
	 
	/// <summary>
	/// Update all the states that are activated.
	/// </summary>
	/// <remarks>
	/// This method is called after the AUpdate and the AFixedUpdate methods. Called once per frame.
	/// </remarks>
	public virtual void KLateUpdate()
	{
		//Call all current active States' Update
		for(var i = 0; i < _priorityLevels.Length; i ++)
		{
			for(var j = 0; j < _priorityLevels[i].Count; j ++)
			{
				if(_currentActiveStates.Contains(_priorityLevels[i][j]))
					_priorityLevels[i][j].KLateUpdate();
			}
		}
	}
	 
	
	/// <summary>
	/// AFixedUpdate updates all the activated states.
	/// </summary>
	/// <remarks>
	/// AFixedUpdate is called only at every physic update, it can be called once per frame, or even one time for each 3~4 frames.<BR>
	/// The variable that determines how many times this method will be called for second (in every frame or not) is the Time.fixedDeltaTime.<BR>
	/// <BR>
	/// It's highly recommended that you apply any physic modification (in rigidbodys or objects with colliders) in this method, not in Update method.
	/// </remarks>
	public virtual void KFixedUpdate()
	{
		//Call all current active States' Update
		for(var i = 0; i < _priorityLevels.Length; i ++)
		{
			for(var j = 0; j < _priorityLevels[i].Count; j ++)
			{
				if(_currentActiveStates.Contains(_priorityLevels[i][j]))
					_priorityLevels[i][j].KFixedUpdate();
			}
		}
	}
	 
	/// <summary>
	/// Call all AOnGUI methods of active states. Method responsable for drawing GUI elements.
	/// </summary>
	/// <remarks>
	/// This method is called for each event that occurs in the frame.<BR>
	/// It can be called more than 3 times in a single frame.<BR>
	/// You should use this method ONLY for drawing GUI elements. Try to leave all GUI calculations or any update you need to get from other classes/objects in Update method.
	/// </remarks>
	public virtual void KOnGUI()
	{	
		//Call all current active States' Update
		for(var i = 0; i < _priorityLevels.Length; i ++)
		{
			for(var j = 0; j < _priorityLevels[i].Count; j ++)
			{
				if(_currentActiveStates.Contains(_priorityLevels[i][j]))
					_priorityLevels[i][j].KOnGUI();
			}
		}
	}
	 
	/// <summary>
	/// Gets the active states.
	/// </summary>
	/// <returns>
	/// A list with the active states.
	/// </returns>
	public virtual List<KState<T>> GetActiveStates()
	{
		return _currentActiveStates;
	}
	 
	/// <summary>
	/// Gets the state.
	/// </summary>
	/// <returns>
	/// A state with matching state type.
	/// </returns>
	/// <param name='p_stateType'>
	/// The type of the state that you want to get.
	/// </param>
	public KState<T> GetState(T p_stateType)
	{
		return _availableStates[p_stateType];
	}
	
	 
	/// <summary>
	/// Check if the given state is active.
	/// </summary>
	/// <returns>
	/// True if the state with the given type is active.
	/// </returns>
	/// <param name='p_type'>
	/// The type of the state that you want to check.
	/// </param>
	public virtual bool IsActive(T p_type)
	{
		bool __found = false;

		foreach(var activeState in _currentActiveStates)
		{
			if(activeState.state.ToString() == p_type.ToString())
				__found = true;
		}
		
		return __found;
	}
	 
	/// <summary>
	/// Disables the given state if it active.
	/// </summary>
	/// <param name='p_state'>
	/// Type of the state that you want to deactive.
	/// </param>
	public virtual void DisableState(T p_state)
	{
		if(_currentActiveStates.Contains(_availableStates[p_state]))
		{
			if(_availableStates[p_state].type != KStateType.PERSISTENT)
			{
				_availableStates[p_state].KOnDisable();
				OnStateDeactivated(_availableStates[p_state]);
				_currentActiveStates.Remove(_availableStates[p_state]);
			}
		}
	}
	 
	/// <summary>
	/// Enables the state with the given type.
	/// </summary>
	/// <param name='p_state'>
	/// The type of the state that you want to enable.
	/// </param>
	public virtual void EnableState(T p_state)
	{
		if(!_currentActiveStates.Contains(_availableStates[p_state]))
		{
			_currentActiveStates.Add(_availableStates[p_state]);
			_availableStates[p_state].KOnEnable();
			OnStateActivated(_availableStates[p_state]);
			currentState = p_state;
		}
	}
	
	/// <summary>
	/// Enables the state with the given type.
	/// </summary>
	/// <param name='p_state'>
	/// The type of the state that you want to enable.
	/// </param>
	/// <param name='p_enableParameter'>
	/// Anything you want to send to the state that you are trying to active.
	/// </param>
	/// <remarks>
	/// Please note that this method don't call the OnEnable in the state.<BR>
	/// This method call the OnEnable(object) in the state. Passing the given object to the state.
	/// </remarks>
	public virtual void EnableState(T p_state, object p_enableParameter)
	{
		if(!_currentActiveStates.Contains(_availableStates[p_state]))
		{
			_currentActiveStates.Add(_availableStates[p_state]);
			_availableStates[p_state].KOnEnable(p_enableParameter);
			OnStateActivated(_availableStates[p_state]);
			currentState = p_state;
		}
	}
	 
	/// <summary>
	/// Disable all states that dont have the same type as the given type and enable the state with the given type if it is not activated.
	/// </summary>
	/// <param name='p_state'>
	/// The type of state that you want to change to.
	/// </param>
	public virtual void ChangeToState(T p_state)
	{
		//Disable all
		for(var i = 0; i < _availableStates.Count; i ++)
		{
			DisableState(_stateTypes[i]);
		}
		
		//Enable the given one	
		EnableState(p_state);
	}
	
	/// <summary>
	/// Disable all states that dont have the same type as the given type and enable the state with the given type if it is not activated.
	/// </summary>
	/// <param name='p_state'>
	/// The type of state that you want to change to.
	/// </param>
	/// <param name='p_enableParameter'>
	/// Anything you want to send to the activated state.
	/// </param>
	/// <remarks>
	/// Please note that this method don't call the OnEnable in the state.<BR>
	/// This method call the OnEnable(object) in the state. Passing the given object to the state.
	/// </remarks>
	public virtual void ChangeToState(T p_state, object p_enableParameter)
	{
		//Disable all
		for(var i = 0; i < _availableStates.Count; i ++)
		{
			DisableState(_stateTypes[i]);
		}
		
		//Enable the given one
		EnableState(p_state, p_enableParameter);
	}
	 
}
