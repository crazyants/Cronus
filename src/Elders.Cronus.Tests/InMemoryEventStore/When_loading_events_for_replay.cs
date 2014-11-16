﻿using Elders.Cronus.DomainModeling;
using Elders.Cronus.EventSourcing;
using Elders.Cronus.EventSourcing.InMemory;
using Elders.Cronus.Tests.TestModel;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elders.Cronus.Tests.InMemoryEventStore
{
    [Subject("AggregateRoot")]
    public class When_loading_events_for_replay
    {
        Establish context = () =>
        {
            versionService = new InMemoryAggregateVersionService();
            eventStoreStorage = new InMemoryEventStoreStorage();
            eventStorePersister = new InMemoryEventStorePersister(eventStoreStorage);
            eventStoreManager = new InMemoryEventStoreStorageManager();
            eventStorePlayer = new InMemoryEventStorePlayer(eventStoreStorage);
            aggregateRepository = new InMemoryAggregateRepository(eventStorePersister, eventStoreStorage, versionService);
            eventStoreManager.CreateStorage();
            aggregateRoot = new TestAggregateRoot(new TestAggregateId());
            aggregateRepository.Save<TestAggregateRoot>(aggregateRoot);
            aggregateRoot.Apply(new TestUpdateEvent(id, "When_build_aggregate_root_from_events"));
            aggregateRepository.Save<TestAggregateRoot>(aggregateRoot);

            secondAggregateRoot = new TestAggregateRoot(new TestAggregateId());
            aggregateRepository.Save<TestAggregateRoot>(secondAggregateRoot);
        };

        Because of = () => events = eventStorePlayer.GetEventsFromStart().ToList();

        It should_load_all_events = () => events.Count.ShouldEqual(3);

        static TestAggregateId id;
        static InMemoryEventStoreStorage eventStoreStorage;
        static IAggregateVersionService versionService;
        static IEventStorePersister eventStorePersister;
        static IEventStoreStorageManager eventStoreManager;
        static IEventStorePlayer eventStorePlayer;
        static IAggregateRepository aggregateRepository;
        static TestAggregateRoot aggregateRoot;
        static TestAggregateRoot secondAggregateRoot;
        static List<IEvent> events;
    }
}