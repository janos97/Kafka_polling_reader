public List<string> GetRecords()
{
    using (var consumer = new ConsumerBuilder<Ignore, string>(_config).Build())
    {
        consumer.Subscribe(_topic);

        var records = new List<string>();

        try
        {
            while (true)
            {
                var cr = consumer.Poll(TimeSpan.FromMilliseconds(100));
                if (cr != null)
                {
                    records.Add(cr.Value);
                }
            }
        }
        catch (ConsumeException e)
        {
            Console.WriteLine($"Error occured: {e.Error.Reason}");
        }

        return records;
    }
}
