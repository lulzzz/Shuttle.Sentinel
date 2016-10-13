import Component from 'can/component/';
import template from './add.stache!';
import resources from 'sentinel/resources';
import Permissions from 'sentinel/permissions';
import state from 'sentinel/state';
import Model from 'sentinel/model';
import validation from 'sentinel/validation';
import localisation from 'sentinel/localisation';

resources.add('queue', { action: 'add', permission: Permissions.Manage.Queues});

export const ViewModel = Model.extend({
    define: {
        uri: {
            value: ''
        },

        uriConstraint: {
            get: function() {
                return validation.get('uri', this.attr('uri'), {
                    uri: {
                        presence: true,
                        uri: true
                    }
                });
            }
        }
    },

    hasErrors: function() {
        return this.attr('uriConstraint');
    },

    add: function() {
        if (this.hasErrors()) {
            return false;
        }

        this.post('queues', {
            uri: this.attr('uri')
        });

        this.close();

        return false;
    },

    close: function() {
        state.goto('queues/list');
    }
});

export default Component.extend({
    tag: 'sentinel-queue-add',
    viewModel: ViewModel,
    template
});