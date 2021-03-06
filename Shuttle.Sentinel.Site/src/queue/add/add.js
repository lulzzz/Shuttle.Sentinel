import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './add.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import localisation from '~/localisation';
import state from '~/state';

resources.add('queue', { action: 'add', permission: Permissions.Manage.Queues });

var queues = new Api({
    endpoint: 'queues/{id}'
});

export const ViewModel = DefineMap.extend(
    'queue',
    {
        init() {
            state.title = localisation.value('queue:list.title');
        },

        uri: {
            default: '',
            get: function(value) {
                var result = value;

                if (!value) {
                    result = state.stack.pop('queue');

                    if (result) {
                        result = result.uri;
                    }
                }

                return result || value;
            },
            validate: {
                presence: true
            }
        },

        add: function() {
            if (!!this.errors()) {
                return false;
            }

            queues.post({
                uri: this.uri
            });

            this.close();

            return false;
        },

        close: function() {
            router.goto({
                resource: 'queue',
                action: 'list'
            });
        }
    }
);

validator(ViewModel);

export default Component.extend({
    tag: 'sentinel-queue-add',
    ViewModel,
    view
});