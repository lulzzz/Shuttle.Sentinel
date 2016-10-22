var Permissions = {
    Manage: {
        DataStores: 'sentinel://data-stores/manage',
        Roles: 'sentinel://roles/manage',
        Users: 'sentinel://users/manage',
        Messages: 'sentinel://messages/manage'
    },
    View: {
        Dashboard: 'sentinel://dashboard/view',
        Subscriptions: 'sentinel://subscriptions/view',
        Queues: 'sentinel://queues/view',
        Roles: 'sentinel://roles/view',
        Users: 'sentinel://users/view'
    }
};

export default Permissions;