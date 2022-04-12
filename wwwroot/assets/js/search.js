
window.showSearchData = (data) => {
    //var KTLayoutQuickSearch = function () {
    //    // Private properties
    //    var _element;
    //    var _offcanvasObject;

    //    // Private functions
    //    var _init = function () {
    //        var header = KTUtil.find(_element, '.offcanvas-header');
    //        var content = KTUtil.find(_element, '.offcanvas-content');
    //        var form = KTUtil.find(_element, '.quick-search-form');
    //        var results = KTUtil.find(_element, '.quick-search-wrapper');

    //        _offcanvasObject = new KTOffcanvas(_element, {
    //            overlay: true,
    //            baseClass: 'offcanvas',
    //            placement: 'right',
    //            closeBy: 'kt_quick_search_close',
    //            toggleBy: 'kt_quick_search_toggle'
    //        });

    //        KTUtil.scrollInit(results, {
    //            disableForMobile: true,
    //            resetHeightOnDestroy: true,
    //            handleWindowResize: true,
    //            height: function () {
    //                var height = parseInt(KTUtil.getViewPort().height);

    //                if (header) {
    //                    height = height - parseInt(KTUtil.actualHeight(header));
    //                    height = height - parseInt(KTUtil.css(header, 'marginTop'));
    //                    height = height - parseInt(KTUtil.css(header, 'marginBottom'));
    //                }

    //                if (content) {
    //                    height = height - parseInt(KTUtil.css(content, 'marginTop'));
    //                    height = height - parseInt(KTUtil.css(content, 'marginBottom'));
    //                }

    //                if (results) {
    //                    height = height - parseInt(KTUtil.actualHeight(form));
    //                    height = height - parseInt(KTUtil.css(form, 'marginTop'));
    //                    height = height - parseInt(KTUtil.css(form, 'marginBottom'));

    //                    height = height - parseInt(KTUtil.css(results, 'marginTop'));
    //                    height = height - parseInt(KTUtil.css(results, 'marginBottom'));
    //                }

    //                height = height - parseInt(KTUtil.css(_element, 'paddingTop'));
    //                height = height - parseInt(KTUtil.css(_element, 'paddingBottom'));

    //                height = height - 2;

    //                return height;
    //            }
    //        });
    //    }

    //    // Public methods
    //    return {
    //        init: function (id) {
    //            _element = KTUtil.getById(id);

    //            if (!_element) {
    //                return;
    //            }

    //            // Initialize
    //            _init();
    //        },

    //        getElement: function () {
    //            return _element;
    //        }
    //    };
    //}();

    //// Webpack support
    //if (true) {
    //    module.exports = KTLayoutQuickSearch;
    //}



    var KTLayoutSearch = function () {
        // Private properties
        var _target;
        var _form;
        var _input;
        var _closeIcon;
        var _resultWrapper;
        var _resultDropdown;
        var _resultDropdownToggle;
        var _closeIconContainer;
        var _inputGroup;
        var _query = '';

        var _hasResult = false;
        var _timeout = false;
        var _isProcessing = false;
        var _requestTimeout = 200; // ajax request fire timeout in milliseconds
        var _spinnerClass = 'spinner spinner-sm spinner-primary';
        var _resultClass = 'quick-search-has-result';
        var _minLength = 2;

        // Private functions
        var _showProgress = function () {
            _isProcessing = true;
            KTUtil.addClass(_closeIconContainer, _spinnerClass);

            if (_closeIcon) {
                KTUtil.hide(_closeIcon);
            }
        }

        var _hideProgress = function () {
            _isProcessing = false;
            KTUtil.removeClass(_closeIconContainer, _spinnerClass);

            if (_closeIcon) {
                if (_input.value.length < _minLength) {
                    KTUtil.hide(_closeIcon);
                } else {
                    KTUtil.show(_closeIcon, 'flex');
                }
            }
        }

        var _showDropdown = function () {
            if (_resultDropdownToggle && !KTUtil.hasClass(_resultDropdown, 'show')) {
                $(_resultDropdownToggle).dropdown('toggle');
                $(_resultDropdownToggle).dropdown('update');
            }
        }

        var _hideDropdown = function () {
            if (_resultDropdownToggle && KTUtil.hasClass(_resultDropdown, 'show')) {
                $(_resultDropdownToggle).dropdown('toggle');
            }
        }

        var _processSearch = function () {
            if (_hasResult && _query === _input.value) {
                _hideProgress();
                KTUtil.addClass(_target, _resultClass);
                _showDropdown();
                KTUtil.scrollUpdate(_resultWrapper);

                return;
            }

            _query = _input.value;

            KTUtil.removeClass(_target, _resultClass);
            _showProgress();
            _hideDropdown();

            if (data) {
                _hasResult = true;
                _hideProgress();
                KTUtil.addClass(_target, _resultClass);
                KTUtil.setHTML(_resultWrapper, data);
                _showDropdown();
                KTUtil.scrollUpdate(_resultWrapper);
            } else {
                _hasResult = false;
                _hideProgress();
                KTUtil.addClass(_target, _resultClass);
                KTUtil.setHTML(_resultWrapper, '<span class="font-weight-bold text-muted">Connection error. Please try again later..</div>');
                _showDropdown();
                KTUtil.scrollUpdate(_resultWrapper);
            }
        }

        var _handleCancel = function (e) {
            _input.value = '';
            _query = '';
            _hasResult = false;
            KTUtil.hide(_closeIcon);
            KTUtil.removeClass(_target, _resultClass);
            _hideDropdown();
        }

        var _handleSearch = function () {
            if (_input.value.length < _minLength) {
                _hideProgress();
                _hideDropdown();

                return;
            }

            if (_isProcessing == true) {
                return;
            }

            if (_timeout) {
                clearTimeout(_timeout);
            }

            _timeout = setTimeout(function () {
                _processSearch();
            }, _requestTimeout);
        }

        // Public methods
        return {
            init: function (id) {
                _target = KTUtil.getById(id);

                if (!_target) {
                    return;
                }

                _form = KTUtil.find(_target, '.quick-search-form');
                _input = KTUtil.find(_target, '.form-control');
                _closeIcon = KTUtil.find(_target, '.quick-search-close');
                _resultWrapper = KTUtil.find(_target, '.quick-search-wrapper');
                _resultDropdown = KTUtil.find(_target, '.dropdown-menu');
                _resultDropdownToggle = KTUtil.find(_target, '[data-toggle="dropdown"]');
                _inputGroup = KTUtil.find(_target, '.input-group');
                _closeIconContainer = KTUtil.find(_target, '.input-group .input-group-append');

                // Attach input keyup handler
                KTUtil.addEvent(_input, 'keyup', _handleSearch);
                KTUtil.addEvent(_input, 'focus', _handleSearch);

                // Prevent enter click
                _form.onkeypress = function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    if (key == 13) {
                        e.preventDefault();
                    }
                }

                KTUtil.addEvent(_closeIcon, 'click', _handleCancel);
            }
        };
    };

    var KTLayoutSearchInline = KTLayoutSearch;
    var KTLayoutSearchOffcanvas = KTLayoutSearch;


    /***/
}