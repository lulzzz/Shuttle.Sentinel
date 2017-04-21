import Permissions from '~/permissions';

var map = [
    {
        href: '#!dashboard',
        text: 'navigation:dashboard',
        permission: Permissions.View.Dashboard
    },
    {
        href: '#!message/manage',
        text: 'navigation:messages',
        permission: Permissions.Manage.Messages
    },
    {
        href: '#!subscription/list',
        text: 'navigation:subscriptions',
        permission: Permissions.View.Subscriptions
    },
    {
        href: '#!datastore/list',
        text: 'navigation:data-stores',
        permission: Permissions.View.DataStores
    },
    {
        href: '#!queue/list',
        text: 'navigation:queues',
        permission: Permissions.View.Queues
    },
    {
        text: 'navigation:system',
        items: [
            {
                href: '#!user/list',
                text: 'user:list.title',
                permission: Permissions.View.Users
            },
            {
                href: '#!role/list',
                text: 'role:list.title',
                permission: Permissions.View.Roles
            }
        ]
    }
];

export default map;