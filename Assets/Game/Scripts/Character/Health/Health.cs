using System.Runtime.Serialization;

public class Health
{
	private int _value;

	public int Value {
		get => _value;
		set
		{
			_value = value;
			
			if (value < 0)
			{
				_value = 0;
			}
			
			if (value > MaxValue)
			{
				_value = MaxValue;
			}
		}
	}

	public int MaxValue { get; }

	public Health(int value, int maxValue)
	{
		MaxValue = maxValue;
		_value = value;
	}
}