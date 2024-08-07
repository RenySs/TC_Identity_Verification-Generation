using System;

public class TCKimlikNoValidator
{
	public static bool Validate(string tckn)
	{
		if (tckn.Length != 11)
		{
			Console.WriteLine("Hata: Kimlik numarası 11 haneli olmalıdır.");
			return false;
		}
		if (!long.TryParse(tckn, out _))
		{
			Console.WriteLine("Hata: Kimlik numarası sadece rakamlardan oluşmalıdır.");
			return false;
		}
		if (tckn[0] == '0')
		{
			Console.WriteLine("Hata: Kimlik numarasının ilk hanesi sıfır (0) olamaz.");
			return false;
		}

		int[] digits = new int[11];
		for (int i = 0; i < 11; i++)
		{
			digits[i] = tckn[i] - '0';
			Console.WriteLine($"{i + 1}. hane: {digits[i]}");
		}

		if (digits[10] % 2 != 0)
		{
			Console.WriteLine("Hata: Kimlik numarasının son hanesi çift sayı olmalıdır.");
			return false;
		}

		int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
		int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

		Console.WriteLine($"1, 3, 5, 7 ve 9. hanelerinin toplamı: {oddSum}");
		Console.WriteLine($"2, 4, 6 ve 8. hanelerinin toplamı: {evenSum}");

		int checkDigit10 = (7 * oddSum - evenSum) % 10;
		int checkDigit11 = (oddSum + evenSum + digits[9]) % 10;

		Console.WriteLine($"(7 * {oddSum}) - {evenSum} % 10 = {checkDigit10} (10. hane kontrolü)");
		Console.WriteLine($"({oddSum} + {evenSum} + {digits[9]}) % 10 = {checkDigit11} (11. hane kontrolü)");

		return digits[9] == checkDigit10 && digits[10] == checkDigit11;
	}

	public static string GenerateValidTCKN()
	{
		Random rnd = new Random();
		int[] digits = new int[11];

		// İlk hanenin sıfır olmaması kontrolü
		digits[0] = rnd.Next(1, 10);

		for (int i = 1; i < 9; i++)
		{
			digits[i] = rnd.Next(0, 10);
		}

		int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
		int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

		digits[9] = (7 * oddSum - evenSum) % 10;
		digits[10] = (oddSum + evenSum + digits[9]) % 10;

		return string.Join("", digits);
	}

	public static void Main(string[] args)
	{
		while (true)
		{
			Console.WriteLine("T.C. Kimlik Numarasını Girin (Çıkmak için 'exit' yazın, Yeni T.C. Kimlik Numarası üretmek için 'generate' yazın):");
			string input = Console.ReadLine();

			if (input.ToLower() == "exit")
			{
				break;
			}

			if (input.ToLower() == "generate")
			{
				string newTCKN = GenerateValidTCKN();
				Console.WriteLine($"Üretilen Geçerli T.C. Kimlik Numarası: {newTCKN}");
			}
			else
			{
				if (Validate(input))
				{
					Console.WriteLine("Geçerli T.C. Kimlik Numarası.");
				}
				else
				{
					Console.WriteLine("Geçersiz T.C. Kimlik Numarası.");
				}
			}
		}
	}
}
