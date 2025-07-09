using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using EventStore.Client;

namespace Infrastructure.EventSourcing
{
    /// <summary>
    /// Serviço para gravar e ler eventos no EventStoreDB.
    /// </summary>
    public class EventStoreService
    {
        private readonly EventStoreClient _client;

        public EventStoreService(EventStoreClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Persiste um evento no EventStoreDB.
        /// </summary>
        public async Task AppendEventAsync(string stream, object @event)
        {
            var data = JsonSerializer.SerializeToUtf8Bytes(@event);
            var evt = new EventData(Uuid.NewUuid(), @event.GetType().Name, data);
            await _client.AppendToStreamAsync(stream, StreamState.Any, new[] { evt });
        }

        /// <summary>
        /// Lê todos os eventos de um stream.
        /// </summary>
        public async Task<IReadOnlyCollection<object>> ReadEventsAsync(string stream)
        {
            var result = new List<object>();
            var events = _client.ReadStreamAsync(Direction.Forwards, stream, StreamPosition.Start);
            await foreach (var resolved in events)
            {
                var json = Encoding.UTF8.GetString(resolved.Event.Data.ToArray());
                // Aqui você pode desserializar para o tipo correto conforme o nome do evento
                result.Add(json);
            }
            return result;
        }
    }
} 