import Swal from 'sweetalert2'

export default class NotificationsService {
  static async confirmAction(text = 'You wont be able to undo this!') {
    try {
      // NOTE isConfirmed is one of the properties on the object of the Swal.fire response which then is set on the variable res - we want to await this because we don't know how long the user to going to take to choose an option on the sweet alert itself.

      const res = await Swal.fire({
        title: 'Are you sure?',
        text: `Do you want to delete this ${text}?`,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      })
      // NOTE we can check here if the user confirmed, denied, clicked off the alert itself, etc.
      if (res.isConfirmed) {
        Swal.fire(
          'Deleted!',
          `Your ${text} has been deleted.`,
          'success'
        )
        return true
      } else {
        return false
      }
    } catch (error) {
    }
  }

  static async confirmRemove(text = 'You wont be able to undo this!') {
    try {
      // NOTE isConfirmed is one of the properties on the object of the Swal.fire response which then is set on the variable res - we want to await this because we don't know how long the user to going to take to choose an option on the sweet alert itself.

      const res = await Swal.fire({
        title: 'Are you sure?',
        text: `Do you want to remove this ${text}?`,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!'
      })
      // NOTE we can check here if the user confirmed, denied, clicked off the alert itself, etc.
      if (res.isConfirmed) {
        Swal.fire(
          'Removed!',
          `Your ${text} has been removed.`,
          'success'
        )
        return true
      } else {
        return false
      }
    } catch (error) {
    }
  }

  static async genericError(text = 'Something went wrong...') {
    try {
      const res = await Swal.fire({
        icon: 'error',
        title: 'Oops..',
        text: text
      })
      if (res.isConfirmed) {
        return true
      } else {
        return false
      }
    } catch (err) {
    }
  }

  static async success(text = 'Success!') {
    try {
      const res = await Swal.fire({
        icon: 'success',
        text: text,
        showConfirmButton: false,
        timer: 1500
      })
      if (res.isConfirmed) {
        return true
      } else {
        return false
      }
    } catch (err) {

    }
  }
}
